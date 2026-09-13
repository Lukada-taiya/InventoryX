using System.Security.Claims;
using InventoryX.Application.Options;
using InventoryX.Application.Services.IServices;
using InventoryX.Domain.Models;
using InventoryX.Domain.Models.Tenancy;
using InventoryX.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace InventoryX.Presentation.Authentication;

public static class GoogleOAuthHandler
{
    public static async Task OnTicketReceived(TicketReceivedContext context)
    {
        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
        var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
        var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
        var db = context.HttpContext.RequestServices.GetRequiredService<AppDbContext>();
        var jwtOptions = context.HttpContext.RequestServices.GetRequiredService<IOptions<JwtOptions>>();
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("InventoryX.Presentation.Authentication.GoogleOAuthHandler");

        var allowedOrigins = configuration.GetSection("Frontend:AllowedOrigins").Get<string[]>() ?? [];
        var returnUrl = "/";
        if (context.Properties?.Items != null &&
            context.Properties.Items.TryGetValue("returnUrl", out var url) &&
            !string.IsNullOrWhiteSpace(url))
        {
            returnUrl = SafeReturnUrl.Normalize(url, allowedOrigins);
        }
        else if (!string.IsNullOrWhiteSpace(context.Properties?.RedirectUri))
        {
            returnUrl = SafeReturnUrl.Normalize(context.Properties.RedirectUri, allowedOrigins);
        }

        var email = context.Principal?.FindFirstValue(ClaimTypes.Email);
        var nameIdentifier = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);

        logger.LogInformation("OAuth callback received for email: {Email}", email);

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(nameIdentifier))
        {
            logger.LogWarning("OAuth callback missing email or nameIdentifier");
            context.ReturnUri = returnUrl;
            return;
        }

        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            logger.LogInformation("Creating new user and tenant for email: {Email}", email);

            string? inputBusinessName = null;
            string? inputCountry = null;
            string? inputCurrency = null;
            string? inputBusinessType = null;

            if (context.Properties?.Items != null)
            {
                context.Properties.Items.TryGetValue("businessName", out inputBusinessName);
                context.Properties.Items.TryGetValue("country", out inputCountry);
                context.Properties.Items.TryGetValue("currency", out inputCurrency);
                context.Properties.Items.TryGetValue("businessType", out inputBusinessType);
            }

            var googleName = context.Principal?.FindFirstValue(ClaimTypes.Name);
            var businessName = !string.IsNullOrWhiteSpace(inputBusinessName)
                ? inputBusinessName
                : !string.IsNullOrWhiteSpace(googleName)
                    ? $"{googleName}'s Organization"
                    : $"{email}'s Workspace";

            var country = !string.IsNullOrWhiteSpace(inputCountry) ? inputCountry.ToUpperInvariant() : "GH";
            var currency = !string.IsNullOrWhiteSpace(inputCurrency) ? inputCurrency.ToUpperInvariant() : "GHS";

            if (!Enum.TryParse<BusinessType>(inputBusinessType, ignoreCase: true, out var businessType))
                businessType = BusinessType.Retail;

            const string initialChecklist =
                """{"createLocation":false,"addProducts":false,"openingStock":false,"inviteUsers":false,"firstSale":false}""";

            var tenant = new Tenant
            {
                Name = businessName,
                Country = country,
                Currency = currency,
                BusinessType = businessType,
                OnboardingChecklist = initialChecklist,
                RequireExpiryOnBatchReceipt = businessType is BusinessType.Food or BusinessType.Pharmacy,
                BillingEmail = email,
            };

            db.Tenants.Add(tenant);
            await db.SaveChangesAsync();

            var ownerRole = await db.AppRoles.FirstOrDefaultAsync(r => r.Name == "Owner");

            user = new User
            {
                TenantId = tenant.Id,
                IsOwner = true,
                RoleId = ownerRole?.Id,
                LocationScope = "*",
                Name = !string.IsNullOrWhiteSpace(googleName) ? googleName : businessName,
            };

            await userManager.SetUserNameAsync(user, email);
            await userManager.SetEmailAsync(user, email);

            var createResult = await userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                logger.LogError("Failed to create user: {Errors}", string.Join(", ", createResult.Errors.Select(e => e.Description)));
                context.ReturnUri = returnUrl;
                return;
            }

            var confirmToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
            await userManager.ConfirmEmailAsync(user, confirmToken);

            var loginInfo = new UserLoginInfo(context.Scheme.Name, nameIdentifier, context.Scheme.DisplayName);
            await userManager.AddLoginAsync(user, loginInfo);

            var professionalPlan = await db.PlanDefinitions
                .FirstOrDefaultAsync(p => p.Tier == PlanTier.Professional && p.IsActive);
            if (professionalPlan != null)
            {
                var now = DateTime.UtcNow;
                db.Subscriptions.Add(new Subscription
                {
                    TenantId = tenant.Id,
                    PlanDefinitionId = professionalPlan.Id,
                    Status = SubscriptionStatus.Trialing,
                    TrialEndsAt = now.AddDays(14),
                    CurrentPeriodStart = now,
                    CurrentPeriodEnd = now.AddDays(14),
                });
                await db.SaveChangesAsync();
            }

            logger.LogInformation("User and tenant created successfully: {Email}, TenantId: {TenantId}", email, tenant.Id);
        }
        else
        {
            logger.LogInformation("User already exists: {Email}", email);

            var existingLogins = await userManager.GetLoginsAsync(user);
            if (!existingLogins.Any(l => l.LoginProvider == context.Scheme.Name && l.ProviderKey == nameIdentifier))
            {
                var loginInfo = new UserLoginInfo(context.Scheme.Name, nameIdentifier, context.Scheme.DisplayName);
                await userManager.AddLoginAsync(user, loginInfo);
                logger.LogInformation("External login linked to existing user: {Email}", email);
            }
        }

        await signInManager.SignInAsync(user, isPersistent: true, authenticationMethod: IdentityConstants.ApplicationScheme);

        var role = user.RoleId is null
            ? null
            : await db.AppRoles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == user.RoleId);
        var tokens = tokenService.CreateTokenPair(user, role);
        AuthSessionCookies.Set(context.HttpContext.Response, tokens.RefreshToken, jwtOptions);

        var redirectParams = new Dictionary<string, string?>
        {
            ["accessToken"] = tokens.AccessToken,
            ["refreshToken"] = tokens.RefreshToken,
            ["accessTokenExpiresAt"] = tokens.AccessTokenExpiresAt.ToUniversalTime().ToString("O"),
        };
        context.ReturnUri = QueryHelpers.AddQueryString(returnUrl, redirectParams);

        logger.LogInformation("User signed in successfully: {Email}", email);
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentAssertions;
using InventoryX.Application.Options;
using InventoryX.Domain.Models;
using InventoryX.Domain.Models.Tenancy;
using InventoryX.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Xunit;

namespace InventoryX.Infrastructure.Tests.Services;

public sealed class JwtTokenServiceTests
{
    private readonly JwtTokenService _service;

    public JwtTokenServiceTests()
    {
        var options = Options.Create(new JwtOptions
        {
            Issuer = "InventoryX.Test",
            Audience = "InventoryX.Client",
            SigningKey = "SuperSecretTestSigningKey1234567890!@#$%",
            AccessTokenMinutes = 60,
            RefreshTokenDays = 7,
        });

        var cache = new MemoryCache(new MemoryCacheOptions());
        _service = new JwtTokenService(options, cache);
    }

    [Fact]
    public void CreateTokenPair_Includes_All_Required_Claims()
    {
        var tenantId = Guid.NewGuid();
        var roleId = Guid.NewGuid();
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Email = "testuser@example.com",
            Name = "Test User",
            TenantId = tenantId,
            IsOwner = true,
            RoleId = roleId,
            LocationScope = "loc-1,loc-2",
            Status = UserStatus.Active,
        };

        var role = new Role
        {
            Id = roleId,
            Name = "Owner",
        };

        var tokenPair = _service.CreateTokenPair(user, role);

        tokenPair.Should().NotBeNull();
        tokenPair.AccessToken.Should().NotBeNullOrEmpty();

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(tokenPair.AccessToken);

        jwtToken.Claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Sub && c.Value == user.Id);
        jwtToken.Claims.Should().Contain(c => c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id);
        jwtToken.Claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Email && c.Value == user.Email);
        jwtToken.Claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Name && c.Value == user.Name);
        jwtToken.Claims.Should().Contain(c => c.Type == "name" && c.Value == user.Name);
        jwtToken.Claims.Should().Contain(c => c.Type == "tenant_id" && c.Value == tenantId.ToString());
        jwtToken.Claims.Should().Contain(c => c.Type == "tenantId" && c.Value == tenantId.ToString());
        jwtToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == "Owner");
        jwtToken.Claims.Should().Contain(c => c.Type == "role" && c.Value == "Owner");
        jwtToken.Claims.Should().Contain(c => c.Type == "role_id" && c.Value == roleId.ToString());
        jwtToken.Claims.Should().Contain(c => c.Type == "is_owner" && c.Value == "true");
        jwtToken.Claims.Should().Contain(c => c.Type == "location_scope" && c.Value == "loc-1,loc-2");
        jwtToken.Claims.Should().Contain(c => c.Type == "status" && c.Value == "Active");
    }
}

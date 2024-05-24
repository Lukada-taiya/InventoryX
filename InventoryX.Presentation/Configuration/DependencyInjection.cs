using InventoryX.Application.Services;
using InventoryX.Application.Services.Common;
using InventoryX.Domain.Models;
using InventoryX.Infrastructure;
using InventoryX.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;

namespace InventoryX.Presentation.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IPurchaseRepository), typeof(PurchaseRepository));
            return services;
        }
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddScoped<IInventoryItemService, InventoryItemService>();
            services.AddScoped<IInventoryItemTypeService, InventoryItemTypeService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddHttpContextAccessor();
            return services;
        }
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            //Add if only there is a cyclical reference
            //    .AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            //});  
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt => {
                opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorisation",
                    Type = SecuritySchemeType.ApiKey
                });
                opt.OperationFilter<SecurityRequirementsOperationFilter>();
            }
            );

            return services;
        }
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddAuthentication();
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddApiEndpoints().AddDefaultTokenProviders();

            //services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            return services;
        }

    }
}

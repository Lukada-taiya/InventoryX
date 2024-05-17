using InventoryX.Application.Services; 
using InventoryX.Infrastructure;
using InventoryX.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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
            return services;
        }
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddTransient<IInventoryItemService, InventoryItemService>();
            return services;
        }
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
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
            services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
            return services;
        }

    }
}

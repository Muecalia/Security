using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Security.Core.Entities;
using Security.Core.Repositories;
using Security.Core.Services;
using Security.Infrastructure.MessageBus;
using Security.Infrastructure.Persistence;
using Security.Infrastructure.Repositories;
using Security.Infrastructure.Services;

namespace Vucan.Security.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) 
        {
            services
                .AddIdentityServices()
                .AddServices()
                ;
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddSingleton<IRabbitMqService, RabbitMqService>();
            services.AddSingleton<IJwtTokenService, JwtTokenService>();

            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            //CONFIG IDENTITY
            services.AddIdentity<Accounts, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<AccountContext>()
            .AddDefaultTokenProviders();

            return services;
        }

    }
}

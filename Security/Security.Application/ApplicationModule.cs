using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Security.Application.Handlers.Account;
using Security.Application.Validators.Account;

namespace Security.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddFluentValidation()
                .AddHandlers()
                //.AddBackgroundService()
                ;
            return services;
        }

        private static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            //services.AddValidatorsFromAssemblyContaining<CreateAccountValidator>();
            services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<CreateAccountValidator>();
            //services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<LoginUserValidator>();

            //builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerRequestValidator>();

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<ChangePasswordHandler>());

            return services;
        }

        private static IServiceCollection AddBackgroundService(this IServiceCollection services)
        {
            //services.AddHostedService<AccountConsumeService>();
            return services;
        }

    }
}

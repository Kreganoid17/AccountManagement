using AccountManagementAPI.HelperServices.Email.Repository;
using AccountManagementAPI.HelperServices.Email.Services;

namespace AccountManagementAPI.HelperServices.Email
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services) 
        {
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}

using AccountManagementAPI.Domains.Accounts.Repository;
using AccountManagementAPI.Domains.Accounts.Services;

namespace AccountManagementAPI.Domains.Accounts;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountsRepository, AccountsRepository>();

        return services;
    }
}

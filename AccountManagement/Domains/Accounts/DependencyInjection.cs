using AccountManagement.Domains.Accounts.Repository;
using AccountManagement.Domains.Accounts.Services;

namespace AccountManagement.Domains.Accounts;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountsRepository, AccountsRepository>();

        return services;
    }
}

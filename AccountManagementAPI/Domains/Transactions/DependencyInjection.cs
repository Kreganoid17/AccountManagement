using AccountManagementAPI.Domains.Transactions.Repository;
using AccountManagementAPI.Domains.Transactions.Services;

namespace AccountManagementAPI.Domains.Transactions;

public static class DependencyInjection
{
    public static IServiceCollection AddTransactionsServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionsRepository, TransactionsRepository>();

        return services;
    }
}

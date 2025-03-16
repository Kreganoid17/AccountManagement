using AccountManagement.Domains.Transactions.Repository;
using AccountManagement.Domains.Transactions.Services;

namespace AccountManagement.Domains.Transactions;

public static class DependencyInjection
{
    public static IServiceCollection AddTransactionsServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionsRepository, TransactionsRepository>();

        return services;
    }
}

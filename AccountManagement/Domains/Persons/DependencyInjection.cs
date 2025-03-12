using AccountManagement.Domains.Persons.Repository;
using AccountManagement.Domains.Persons.Services;

namespace AccountManagement.Domains.Persons;

public static class DependencyInjection
{
    public static IServiceCollection AddPersonsServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonsRepository, PersonsRepository>();

        return services;
    }
}

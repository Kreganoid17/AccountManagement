using AccountManagementAPI.Domains.Persons.Repository;
using AccountManagementAPI.Domains.Persons.Services;

namespace AccountManagementAPI.Domains.Persons;

public static class DependencyInjection
{
    public static IServiceCollection AddPersonsServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonsRepository, PersonsRepository>();

        return services;
    }
}

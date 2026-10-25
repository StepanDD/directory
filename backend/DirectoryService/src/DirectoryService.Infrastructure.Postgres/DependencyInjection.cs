using DirectoryService.Core.Departments;
using DirectoryService.Core.Locations;
using DirectoryService.Infrastructure.Postgres.DataBase;
using DirectoryService.Infrastructure.Postgres.Departments;
using DirectoryService.Infrastructure.Postgres.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryService.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructurePostgresServices(this IServiceCollection services)
    {
        services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
        services.AddScoped<ILocationsRepository, LocationsRepository>();
        //services.AddScoped<ILocationsRepository, LocationsRepositoryDapper>();
        services.AddSingleton<IConnectionFactory, NpgsqlConnectionFactory>();
        return services;
    }
}

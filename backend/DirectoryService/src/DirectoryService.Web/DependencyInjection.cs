using System;
using DirectoryService.Core;
using DirectoryService.Infrastructure.Postgres;
using DirectoryService.Presenters;

namespace DirectoryService.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services)
    {
        return services
            .AddWebDependencies()
            .AddCoreServices()
            .AddInfrastructurePostgresServices();
    }
    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddOpenApi();

        services.AddControllers().AddApplicationPart(typeof(DepartmentsController).Assembly);

        services.AddHealthChecks();

        return services;
    }
}

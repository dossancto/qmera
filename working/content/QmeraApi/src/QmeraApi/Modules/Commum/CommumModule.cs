using QmeraApi.Modules.Commum.Adapters.Monitoring;
using QmeraApi.Modules.Commum.Configuration.API;
using QmeraApi.Modules.Commum.Configuration.Databases;

namespace QmeraApi.Modules.Commum;

public static class CommumModule
{
    public static IServiceCollection AddCommumModule(this IServiceCollection services)
    {
        services.AddDatabaseConfiguration();
        services.AddMonitoring();
        services.AddApiConfiguration();

        return services;
    }

    public static WebApplication UseCommumModule(this WebApplication app)
    {
        app.UseApiConfiguration();

        return app;
    }
}
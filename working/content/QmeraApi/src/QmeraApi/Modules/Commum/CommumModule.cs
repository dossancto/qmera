using QmeraApi.Modules.Commum.Adapters.Monitoring;
using QmeraApi.Modules.Commum.Configuration.Databases;

namespace QmeraApi.Modules.Commum;

public static class CommumModule
{
    public static IServiceCollection AddCommumModule(this IServiceCollection services)
    {
        services.AddDatabaseConfiguration();
        services.AddMonitoring();

        return services;
    }
}
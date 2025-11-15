using Qmera.Blazor.Modules.Commum.Adapters.Monitoring;

namespace Qmera.Blazor.Modules.Commum;

public static class CommumModule
{
    public static IServiceCollection AddCommumModule(this IServiceCollection services)
    {
        services.AddMonitoring();

        return services;
    }

    public static WebApplication UseCommumModule(this WebApplication app)
    {
        return app;
    }
}
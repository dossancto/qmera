using Microsoft.EntityFrameworkCore;

using Qmera.Blazor.Data;

namespace Qmera.Blazor.Modules.Commum.Configuration.Databases;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(AppEnv.DATABASES.SQLITE.DATASOURCE.NotNull());
        });

        return services;
    }
}
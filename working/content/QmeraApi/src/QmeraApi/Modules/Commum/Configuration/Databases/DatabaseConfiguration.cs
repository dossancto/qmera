using Microsoft.EntityFrameworkCore;

using QmeraApi.Modules.Commum.Adapters.Databases;

namespace QmeraApi.Modules.Commum.Configuration.Databases;

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
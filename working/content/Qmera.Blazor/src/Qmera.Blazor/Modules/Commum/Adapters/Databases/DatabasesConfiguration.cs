using Microsoft.EntityFrameworkCore;
using Qmera.Blazor.Data;

namespace Qmera.Blazor.Modules.Commum.Adapters.Databases;

public static class DatabasesConfiguration
{
    public static IServiceCollection AddDatabasesConfiguration(this IServiceCollection services)
    {
        var connectionString = AppEnv.DATABASES.SQLITE.DATASOURCE.NotNull();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        return services;
    }
}
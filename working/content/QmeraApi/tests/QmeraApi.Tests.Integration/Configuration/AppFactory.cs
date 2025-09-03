using System.Data.Common;
using DotNetEnv;
using QmeraApi.Modules.Commum.Adapters.Databases;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;

namespace QmeraApi.Tests.Integration.Configuration;

public class AppFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    public AppFactory()
    {
        Env.TraversePath().Load();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(IDbContextOptionsConfiguration<ApplicationDbContext>));

            services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbConnection));

            services.Remove(dbConnectionDescriptor);

            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection(AppEnv.TEST.DATABASES.SQLITE.DATASOURCE.NotNull());
                connection.Open();

                return connection;
            });

            services.AddDbContext<ApplicationDbContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });

            var dbContext = services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
        });

        builder.UseEnvironment("Development");
    }
}
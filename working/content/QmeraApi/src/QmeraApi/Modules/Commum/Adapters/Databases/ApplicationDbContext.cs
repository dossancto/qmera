using Microsoft.EntityFrameworkCore;

using QmeraApi.Modules.Todos.Models;

namespace QmeraApi.Modules.Commum.Adapters.Databases;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<TodoModel> Todos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
    }
}
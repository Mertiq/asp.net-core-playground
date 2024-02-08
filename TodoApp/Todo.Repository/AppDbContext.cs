using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Task = Todo.Core.Entities.Task;

namespace Todo.Repository;

public class AppDbContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application;
using TodoApp.Domain.Tasks;
using TodoApp.Domain.Users;

namespace TodoApp.Infrastructure.Data;
public class ApplicationDbContext(DbContextOptions options) : DbContext(options), IApplicationDbContext
{
    public DbSet<TodoItem> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

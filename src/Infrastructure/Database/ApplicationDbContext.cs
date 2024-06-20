using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application;
using TodoApp.Domain.Tasks;
using TodoApp.Domain.Users;
using TodoApp.Infrastructure.Database.Models;

namespace TodoApp.Infrastructure.Database;
public class ApplicationDbContext(DbContextOptions options) : DbContext(options), IApplicationDbContext
{
    public DbSet<OutboxMessage> Outbox { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

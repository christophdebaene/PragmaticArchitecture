using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Tasks;
using TodoApp.Domain.Users;

namespace TodoApp.Application;
public interface IApplicationDbContext
{
    public DbSet<TodoItem> Tasks { get; }
    public DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

using Bricks.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoApp.Domain.Tasks;
using TodoApp.Domain.Users;

namespace TodoApp.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    private readonly IUserContext _userContext;
    public DbSet<TodoItem> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public ApplicationDbContext(DbContextOptions options, IUserContext userContext) : base(options)
    {
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();

        var timestamp = SystemClock.GetUtcNow();

        foreach (var entity in ChangeTracker.Entries())
        {
            if (entity.Entity is IAuditable auditable)
            {
                if (entity.State == EntityState.Added || entity.State == EntityState.Modified)
                {
                    auditable.Audit.Modified = timestamp;
                    auditable.Audit.ModifiedBy = _userContext.CurrentUser.Id.ToString();

                    if (entity.State == EntityState.Added)
                    {
                        auditable.Audit.Created = timestamp;
                        auditable.Audit.CreatedBy = _userContext.CurrentUser.Id.ToString();
                    }
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}

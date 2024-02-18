using Bricks.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TodoApp.Domain.Users;

namespace TodoApp.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor(IUserContext userContext, TimeProvider timeProvider) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateAuditInfo(eventData.Context);
        return base.SavingChanges(eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateAuditInfo(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    public void UpdateAuditInfo(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        context.ChangeTracker.DetectChanges();

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is IAuditable auditable)
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    var timestamp = timeProvider.GetUtcNow(); ;

                    auditable.Audit.Modified = timestamp;
                    auditable.Audit.ModifiedBy = userContext.CurrentUser.Id.ToString();

                    if (entry.State == EntityState.Added)
                    {
                        auditable.Audit.Created = timeProvider.GetUtcNow(); ;
                        auditable.Audit.CreatedBy = userContext.CurrentUser.Id.ToString();
                    }
                }
            }
        }
    }
}


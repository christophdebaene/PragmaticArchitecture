using Bricks.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TodoApp.Domain.Users;

namespace TodoApp.Infrastructure.Database.Interceptors;

public class AuditableInterceptor(IUserContext userContext, TimeProvider timeProvider) : SaveChangesInterceptor
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
                    var timestamp = timeProvider.GetUtcNow();
                    var userId = userContext.CurrentUser.Id.ToString();

                    auditable.Audit.Modified = timestamp;
                    auditable.Audit.ModifiedBy = userId;

                    if (entry.State == EntityState.Added)
                    {
                        auditable.Audit.Created = timestamp;
                        auditable.Audit.CreatedBy = userId;
                    }
                }
            }
        }
    }
}


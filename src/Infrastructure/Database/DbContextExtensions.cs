using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TodoApp.Infrastructure.Database;
public static class DbContextExtensions
{    
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
        =>  entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}

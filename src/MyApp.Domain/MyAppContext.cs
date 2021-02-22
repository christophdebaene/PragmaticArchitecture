using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Shared;
using MyApp.Domain.Tasks;
using MyApp.Domain.Users;

namespace MyApp.Domain
{
    public class MyAppContext : DbContext
    {
        private readonly IUserContext _userContext;

        public DbSet<Todo> Tasks { get; set; }
        public MyAppContext(DbContextOptions options, IUserContext userContext) : base(options)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var timestamp = SystemClock.GetUtcNow();

            ChangeTracker.DetectChanges();

            foreach (var entity in ChangeTracker.Entries())
            {
                if (entity.Entity is IAuditable auditable && (entity.State == EntityState.Added || entity.State == EntityState.Modified))
                {
                    auditable.Audit.Modified = timestamp;
                    auditable.Audit.ModifiedBy = _userContext.CurrentUser.Name;

                    if (entity.State == EntityState.Added)
                    {
                        auditable.Audit.Created = timestamp;
                        auditable.Audit.CreatedBy = _userContext.CurrentUser.Name;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

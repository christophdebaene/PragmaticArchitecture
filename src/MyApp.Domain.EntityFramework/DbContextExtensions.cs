using System.Data.Entity;

namespace MyApp.Domain.EntityFramework
{
    public static class DbContextExtensions
    {
        public static DbContext AsQueryContext(this DbContext context)
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;
            return context;
        }

        public static DbContext AsCommandContext(this DbContext context)
        {
            context.Configuration.AutoDetectChangesEnabled = true;
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = true;
            context.Configuration.ValidateOnSaveEnabled = true;
            return context;
        }
    }
}
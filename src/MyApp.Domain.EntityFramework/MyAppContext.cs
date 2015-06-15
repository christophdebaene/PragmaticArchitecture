using MyApp.Domain.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MyApp.Domain.EntityFramework
{
    public class MyAppContext : DbContext
    {
        public const string DefaultConnectionString = "MyApp";

        public DbSet<Task> TaskSet { get; set; }

        public MyAppContext()
            : base(DefaultConnectionString)
        {
            Database.SetInitializer(new MyAppContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<string>()
                .Where(x => x.Name == "Id")
                .Configure(x => x.IsKey());
        }
    }
}
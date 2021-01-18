using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Model;

namespace MyApp.Domain.Model
{
    public class MyAppContext : DbContext
    {        
        public DbSet<Todo> Todo { get; set; }

        public MyAppContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
        }
    }
}

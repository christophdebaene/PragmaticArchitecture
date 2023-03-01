using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain;

namespace MyApp.Application.Infrastructure
{
    public interface IDbConnectionFactory
    {
        DbConnection GetConnection();
    }
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly MyAppContext _context;
        public DbConnectionFactory(MyAppContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }
        public DbConnection GetConnection()
        {
            return _context.Database.GetDbConnection();
        }
    }
}

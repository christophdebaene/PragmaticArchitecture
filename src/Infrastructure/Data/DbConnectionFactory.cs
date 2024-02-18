using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application;

namespace TodoApp.Infrastructure.Data;
public class DbConnectionFactory(ApplicationDbContext context) : IDbConnectionFactory
{
    public DbConnection GetConnection()
    {
        return context.Database.GetDbConnection();
    }
}

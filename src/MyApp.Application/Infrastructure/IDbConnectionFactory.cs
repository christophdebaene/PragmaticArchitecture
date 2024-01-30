using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain;

namespace MyApp.Application.Infrastructure;
public interface IDbConnectionFactory
{
    DbConnection GetConnection();
}
public class DbConnectionFactory(MyAppContext context) : IDbConnectionFactory
{    
    public DbConnection GetConnection()
    {
        return context.Database.GetDbConnection();
    }
}

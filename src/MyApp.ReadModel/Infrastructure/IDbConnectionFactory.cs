using System.Data.Common;

namespace MyApp.ReadModel.Infrastructure
{
    public interface IDbConnectionFactory
    {
        DbConnection CreateDbConnection();
    }
}

using System.Data;

namespace MyApp.ReadModel.Infrastructure
{
    public interface IConnectionProvider
    {
        IDbConnection GetOpenConnection();
    }
}
using System.Data;

namespace MyApp.ReadModel.Handlers
{
    public interface IConnectionProvider
    {
        IDbConnection GetOpenConnection();
    }
}
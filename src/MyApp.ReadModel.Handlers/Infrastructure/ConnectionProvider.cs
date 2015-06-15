using System.Configuration;
using System.Data.SqlClient;

namespace MyApp.ReadModel.Handlers
{
    public class MyAppConnectionProvider : IConnectionProvider
    {
        public System.Data.IDbConnection GetOpenConnection()
        {            
            var connectionString = ConfigurationManager.ConnectionStrings["MyApp"].ConnectionString;
            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MyApp.ReadModel.Infrastructure
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly IConfiguration _configuration;
        public ConnectionProvider(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
        }
        public System.Data.IDbConnection GetOpenConnection()
        {
            var connectionString = _configuration.GetConnectionString("MyApp");
            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}

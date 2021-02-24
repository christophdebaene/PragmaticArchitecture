using System;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using MyApp.ReadModel.Infrastructure;
using StackExchange.Profiling;

namespace MyApp.Application.Bootstrapper
{
    public class ProfiledDbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IFeatureManager _featureManager;
        public ProfiledDbConnectionFactory(IConfiguration configuration, IFeatureManager featureManager)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _featureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
        }
        public DbConnection CreateDbConnection()
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("MyApp"));

            if (_featureManager.IsEnabledAsync("MiniProfiler").Result)
                return new StackExchange.Profiling.Data.ProfiledDbConnection(connection, MiniProfiler.Current);

            return connection;
        }
    }
}

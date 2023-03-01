using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using MyApp.Application.Infrastructure;
using MyApp.Domain;
using StackExchange.Profiling;

namespace MyApp.Application.Bootstrapper
{
    public class ProfiledDbConnectionFactory : IDbConnectionFactory
    {
        private readonly MyAppContext _context;
        private readonly IFeatureManager _featureManager;
        public ProfiledDbConnectionFactory(MyAppContext context, IFeatureManager featureManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _featureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
        }
        public DbConnection GetConnection()
        {
            var connection = _context.Database.GetDbConnection();

            if (_featureManager.IsEnabledAsync("MiniProfiler").GetAwaiter().GetResult())
                connection = new StackExchange.Profiling.Data.ProfiledDbConnection(connection, MiniProfiler.Current);

            return connection;
        }
    }
}

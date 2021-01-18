using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Model;
using MyApp.ReadModel.Handlers;
using System.Collections.Generic;
using System.Reflection;

namespace MyApp.Application.Bootstrapper
{
    public static class ServiceCollectionsExtensions
    {
        static readonly List<Assembly> _assemblies = new List<Assembly>
        {
            Assembly.Load("MyApp.Application"),
            Assembly.Load("MyApp.ReadModel.Handlers")
        };
        public static IServiceCollection AddMyApp(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddMediatR(_assemblies.ToArray());

            collection.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            collection.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
            collection.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
            
            collection.AddSingleton<ISystemClock, SystemClock>();
            collection.AddSingleton<IConnectionProvider, ConnectionProvider>();

            collection.AddDbContext<MyAppContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MyApp")));

            return collection;
        }
    }
}
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Model;
using MyApp.ReadModel.Infrastructure;
using System.Collections.Generic;
using System.Reflection;

namespace MyApp.Application.Bootstrapper
{
    public static class ServiceCollectionsExtensions
    {
        static readonly List<Assembly> _assemblies = new List<Assembly>
        {
            Assembly.Load("MyApp.Application"),
            Assembly.Load("MyApp.ReadModel")
        };
        public static IServiceCollection AddMyApp(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .ConfigureMediatR(_assemblies)
                .ConfigureFluentValidaton(_assemblies)
                .ConfigureDatabase(configuration);
                                    
            return services;
        }
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddSingleton<IConnectionProvider, ConnectionProvider>();
            services.AddDbContext<MyAppContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MyApp")));

            return services;
        }        
        public static IServiceCollection ConfigureMediatR(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddMediatR(_assemblies.ToArray());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

            return services;
        }
        public static IServiceCollection ConfigureFluentValidaton(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            foreach (var result in AssemblyScanner.FindValidatorsInAssemblies(assemblies))
                services.AddTransient(result.InterfaceType, result.ValidatorType);

            return services;
        }
    }
}
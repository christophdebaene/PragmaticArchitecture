using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain;
using MyApp.Domain.Users;
using MyApp.ReadModel.Infrastructure;

namespace MyApp.Application.Bootstrapper
{
    public static class ServiceCollectionsExtensions
    {
        static readonly List<Assembly> s_assemblies = new List<Assembly>
        {
            Assembly.Load("MyApp.Application"),
            Assembly.Load("MyApp.ReadModel")
        };
        public static IServiceCollection AddMyApp(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .ConfigureMediatR(s_assemblies)
                .ConfigureFluentValidaton(s_assemblies)
                .ConfigureDatabase(configuration)
                .ConfigureDomain();
        }
        static IServiceCollection ConfigureDomain(this IServiceCollection services)
        {
            return services
                .AddTransient<IUserRepository, MockUserRepository>();
        }
        static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddSingleton<IConnectionProvider, ConnectionProvider>()
                .AddDbContext<MyAppContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("MyApp")));
        }
        static IServiceCollection ConfigureMediatR(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            return services
                .AddMediatR(assemblies.ToArray())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        }
        static IServiceCollection ConfigureFluentValidaton(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            foreach (var result in AssemblyScanner.FindValidatorsInAssemblies(assemblies))
                services.AddTransient(result.InterfaceType, result.ValidatorType);

            return services;
        }
    }
}

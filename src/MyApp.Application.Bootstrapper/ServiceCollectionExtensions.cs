using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
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
            Assembly.Load("MyApp.Domain"),
            Assembly.Load("MyApp.ReadModel")
        };
        public static IServiceCollection AddMyApp(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .ConfigureMediatR(s_assemblies)
                .ConfigureFluentValidaton(s_assemblies)
                .ConfigureFeatures()
                .ConfigureDatabase(configuration)
                .ConfigureDomain();
        }
        static IServiceCollection ConfigureFeatures(this IServiceCollection services)
        {
            services.AddFeatureManagement();
            return services;
        }
        static IServiceCollection ConfigureDomain(this IServiceCollection services)
        {
            return services
                .AddTransient<IUserRepository, MockUserRepository>();
        }
        static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddSingleton<IDbConnectionFactory, ProfiledDbConnectionFactory>()
                .AddDbContext<MyAppContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("MyApp")));
        }
        static IServiceCollection ConfigureMediatR(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            return services
                .AddMediatR(assemblies.ToArray())
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(MiniProfilerBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        }
        static IServiceCollection ConfigureFluentValidaton(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            foreach (var result in AssemblyScanner.FindValidatorsInAssemblies(assemblies))
                services.AddTransient(result.InterfaceType, result.ValidatorType);

            return services;
        }
    }
}

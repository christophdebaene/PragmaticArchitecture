using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using MyApp.Application.Infrastructure;
using MyApp.Domain;

namespace MyApp.Bootstrapper;
public static class ServiceCollectionsExtensions
{
    static readonly List<Assembly> s_assemblies = new()
    {
        Assembly.Load("MyApp.Application"),
        Assembly.Load("MyApp.Domain")
    };
    public static IServiceCollection AddMyApp(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .ConfigureMediatR(s_assemblies)
            .ConfigureFluentValidaton(s_assemblies)
            .ConfigureFeatures()
            .ConfigureEntityFramework(configuration.GetConnectionString("MyApp"));
    }
    static IServiceCollection ConfigureFeatures(this IServiceCollection services)
    {
        services.AddFeatureManagement();
        return services;
    }
    public static IServiceCollection ConfigureEntityFramework(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<MyAppContext>((x) =>
        {
            x.UseSqlServer(connectionString);
            x.EnableSensitiveDataLogging(true);
            x.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        });

        //services.AddTransient<IDbConnectionFactory, ProfiledDbConnectionFactory>();
        services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
        //services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
    static IServiceCollection ConfigureMediatR(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        return services
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assemblies.ToArray())
                    .AddOpenBehavior(typeof(LoggingBehavior<,>))
                    .AddOpenBehavior(typeof(MiniProfilerBehavior<,>))
                    .AddOpenBehavior(typeof(ValidationBehavior<,>))
                    .AddOpenBehavior(typeof(TransactionBehavior<,>))
                    .AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            });

    }
    static IServiceCollection ConfigureFluentValidaton(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        foreach (var result in AssemblyScanner.FindValidatorsInAssemblies(assemblies))
            services.AddTransient(result.InterfaceType, result.ValidatorType);

        return services;
    }
}

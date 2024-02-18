using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using TodoApp.Application;
using TodoApp.Infrastructure.Behaviors;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure;
public static class ServiceCollectionsExtensions
{
    static readonly List<Assembly> s_assemblies =
    [
        Assembly.Load("TodoApp.Application"),
        Assembly.Load("TodoApp.Domain")
    ];
    public static IServiceCollection AddTodoApp(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .ConfigureMediatR(s_assemblies)
            .ConfigureFluentValidation(s_assemblies)
            .ConfigureFeatures()
            .ConfigureEntityFramework(configuration.GetConnectionString("TodoApp"));
    }
    static IServiceCollection ConfigureFeatures(this IServiceCollection services)
    {
        services.AddFeatureManagement();
        return services;
    }
    public static IServiceCollection ConfigureEntityFramework(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>((x) =>
        {
            x.UseSqlServer(connectionString);            
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
    static IServiceCollection ConfigureFluentValidation(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        foreach (var result in AssemblyScanner.FindValidatorsInAssemblies(assemblies))
            services.AddTransient(result.InterfaceType, result.ValidatorType);

        return services;
    }
}

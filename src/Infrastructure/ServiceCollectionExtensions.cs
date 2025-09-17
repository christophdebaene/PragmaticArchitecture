using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Quartz;
using TodoApp.Application;
using TodoApp.Infrastructure.Behaviors;
using TodoApp.Infrastructure.Database;
using TodoApp.Infrastructure.Database.Interceptors;
using TodoApp.Infrastructure.Jobs;

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
            .ConfigureMediator(s_assemblies)
            .ConfigureFluentValidation(s_assemblies)
            //.ConfigureJobs()
            .ConfigureFeatures()
            .ConfigureEntityFramework(configuration.GetConnectionString("TodoApp"));
    }
    static IServiceCollection ConfigureFeatures(this IServiceCollection services)
    {
        services.AddFeatureManagement();
        return services;
    }
    static IServiceCollection ConfigureJobs(this IServiceCollection services)
    {
        var jobKey = new JobKey(nameof(OutboxMessageJob));

        return services.AddQuartz(configure =>
        {
            configure.AddJob<OutboxMessageJob>(jobKey)
                .AddTrigger(trigger => trigger.ForJob(jobKey)
                .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10).RepeatForever()));
        })
        .AddQuartzHostedService();
    }
    public static IServiceCollection ConfigureEntityFramework(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton(_ => TimeProvider.System);
        services.AddScoped<AuditableInterceptor>();
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(connectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            options.AddInterceptors(serviceProvider.GetRequiredService<AuditableInterceptor>());
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
        //services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
    static IServiceCollection ConfigureMediator(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        return services
            .AddMediator(options =>
            {
                options.ServiceLifetime = ServiceLifetime.Scoped;
                options.PipelineBehaviors =
                [
                    typeof(LoggingBehavior<,>),
                    typeof(MiniProfilerBehavior<,>),
                    typeof(ValidationBehavior<,>),
                    typeof(TransactionBehavior<,>),
                    typeof(UnitOfWorkBehavior<,>)
                ];
            });
    }
    static IServiceCollection ConfigureFluentValidation(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        foreach (var result in AssemblyScanner.FindValidatorsInAssemblies(assemblies))
            services.AddTransient(result.InterfaceType, result.ValidatorType);

        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        return services;
    }
}

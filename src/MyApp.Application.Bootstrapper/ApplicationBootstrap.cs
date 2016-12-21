using FluentValidation;
using MyApp.Domain.EntityFramework;
using MyApp.Domain.Model;
using MyApp.Domain.Repositories;
using MyApp.ReadModel.Handlers;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.File;
using SimpleInjector;
using SlickBus;
using System;
using System.Collections.Generic;

namespace MyApp.Application.Bootstrapper
{
    public class ApplicationBootstrap
    {
        public static Container Create(ApplicationBootstrapConfig config)
        {
            var container = new Container();
            container.Options.AllowOverridingRegistrations = true;
            container.Register<IServiceProvider>(() => container);

            ConfigureSerilog();

            container.RegisterMediator(config.Assemblies.ToArray());
            container.Register<ITaskRepository, EfTaskRepository>();
            container.Register<IUnitOfWork>(() => new UnitOfWork(new MyAppContext().AsCommandContext()), config.UnitOfWorkLifestyle);
            container.Register<IQueryContext>(() => new QueryContext(new MyAppContext().AsQueryContext()), config.UnitOfWorkLifestyle);
            container.RegisterSingleton<IConnectionProvider, ConnectionProvider>();
            container.RegisterSingleton<ISystemClock, SystemClock>();
            container.RegisterCollection(typeof(IValidator<>), config.Assemblies);

            var commandDecorators = new List<Type>
            {
                typeof(UnitOfWorkHandler<,>),
                typeof(TransactionHandler<,>),
                typeof(ValidatorHandler<,>),
                typeof(LogHandler<,>)
            };

            container.RegisterRequestHandlerDecorators(GetCommandDecorators(), context =>
            {
                var argument = context.ServiceType.GetGenericArguments()[0];
                return argument.Namespace.EndsWith("Commands");
            });

            container.RegisterRequestHandlerDecorators(GetQueryDecorators(), context =>
            {
                var argument = context.ServiceType.GetGenericArguments()[0];
                return argument.Namespace.EndsWith("Queries");
            });

            return container;
        }

        private static IEnumerable<Type> GetCommandDecorators()
        {
            yield return typeof(UnitOfWorkHandler<,>);
            yield return typeof(TransactionHandler<,>);
            yield return typeof(ValidatorHandler<,>);
            yield return typeof(LogHandler<,>);
        }

        private static IEnumerable<Type> GetQueryDecorators()
        {
            yield return typeof(ValidatorHandler<,>);
            yield return typeof(StopwatchHandler<,>);
        }

        private static void ConfigureSerilog()
        {
            Log.Logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
                .WriteTo.File(new JsonFormatter(), @"myapp-application.json")
                .WriteTo.File(@"myapp-application.txt")
              .CreateLogger();
        }
    }
}
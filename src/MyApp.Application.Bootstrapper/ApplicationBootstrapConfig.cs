using SimpleInjector;
using SimpleInjector.Extensions.LifetimeScoping;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MyApp.Application.Bootstrapper
{
    public class ApplicationBootstrapConfig
    {
        public Lifestyle UnitOfWorkLifestyle { get; set; }
        public List<Assembly> Assemblies { get; set; }

        public static ApplicationBootstrapConfig CreateDefault()
        {
            return CreateDefault<ApplicationBootstrapConfig>();
        }

        public static T CreateDefault<T>() where T : ApplicationBootstrapConfig
        {
            var config = Activator.CreateInstance<T>();
            config.UnitOfWorkLifestyle = new LifetimeScopeLifestyle();

            config.Assemblies = new List<Assembly>
                {
                    typeof(MyApp.Application.Bootstrapper.ApplicationBootstrap).Assembly, // MyApp.Application.Bootstrapper
                    typeof(MyApp.Application.Commands.CompleteTask).Assembly, // MyApp.Application
                    typeof(MyApp.Domain.Model.Task).Assembly, // MyApp.Domain
                    typeof(MyApp.Domain.EntityFramework.IUnitOfWork).Assembly, // MyApp.Domain.EntityFramework
                    typeof(MyApp.ReadModel.Queries.GetTasks).Assembly, // MyApp.ReadModel
                    typeof(MyApp.ReadModel.Handlers.IQueryContext).Assembly, // MyApp.ReadModel.Handlers
                };

            //config.Assemblies = new List<Assembly>
            //    {
            //         Assembly.Load("MyApp.Application"),
            //         Assembly.Load("MyApp.Domain"),
            //         Assembly.Load("MyApp.Domain.EntityFramework"),
            //         Assembly.Load("MyApp.ReadModel"),
            //         Assembly.Load("MyApp.ReadModel.Handlers")
            //    };

            return config;
        }
    }
}
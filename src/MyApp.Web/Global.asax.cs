using MyApp.Domain.EntityFramework;
using MyApp.Web.Bootstrapper;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyApp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Environment.SetEnvironmentVariable("BASEDIR", AppDomain.CurrentDomain.BaseDirectory);

            ConfigureEntityFramework();
            ConfigureContainer();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void ConfigureEntityFramework()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<MyAppContext>());

            var context = new MyAppContext();
            context.Database.CreateIfNotExists();
        }

        private void ConfigureContainer()
        {
            var container = WebBootstrap.Create(WebBootstrapConfig.CreateDefault());
            container.Verify();

            System.Web.Mvc.DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            //System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
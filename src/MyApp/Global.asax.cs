using MyApp.Bootstrapper;
using MyApp.Domain.EntityFramework;
using SimpleInjector.Integration.Web.Mvc;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<MyAppContext>());

            ConfigureContainer();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var context = new MyAppContext();
            context.Database.CreateIfNotExists();
        }

        protected void ConfigureContainer()
        {
            var container = WebBootstrap.Create(WebBootstrapConfig.CreateDefault());
            container.Verify();

            System.Web.Mvc.DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            //System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
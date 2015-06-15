using MyApp.Application.Bootstrapper;
using SimpleInjector.Integration.Web;

namespace MyApp.Web.Bootstrapper
{
    public class WebBootstrapConfig : ApplicationBootstrapConfig
    {
        public static new WebBootstrapConfig CreateDefault()
        {
            var config = ApplicationBootstrapConfig.CreateDefault<WebBootstrapConfig>();
            config.UnitOfWorkLifestyle = new WebRequestLifestyle();
            return config;
        }
    }
}
using MyApp.Application.Bootstrapper;
using SimpleInjector.Integration.Web;

namespace MyApp.Bootstrapper
{
    public class WebBootstrapConfig : ApplicationBootstrapConfig
    {
        public static WebBootstrapConfig CreateDefault()
        {
            var config = ApplicationBootstrapConfig.CreateDefault<WebBootstrapConfig>();
            config.UnitOfWorkLifestyle = new WebRequestLifestyle();
            return config;
        }
    }
}
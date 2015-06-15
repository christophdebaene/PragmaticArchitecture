using MyApp.Application.Bootstrapper;
using SimpleInjector;

namespace MyApp.Bootstrapper
{
    public class WebBootstrap
    {
        public static Container Create(WebBootstrapConfig config)
        {
            var container = ApplicationBootstrap.Create(config);
            container.RegisterMvcControllers(typeof(WebBootstrap).Assembly);
            return container;
        }
    }
}
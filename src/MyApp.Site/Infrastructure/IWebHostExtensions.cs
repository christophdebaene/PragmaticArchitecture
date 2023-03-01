using Microsoft.EntityFrameworkCore;

namespace MyApp.Site.Infrastructure
{
    public static class IWebHostExtensions
    {
        public static IWebHost CreateDatabase<TContext>(this IWebHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<TContext>();

                seeder(context, services);
            }

            return host;
        }
    }
}

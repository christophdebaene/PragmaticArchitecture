using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Bootstrapper;
using MyApp.Domain.Users;
using Terminal.CommandBus;
using Terminal.Infrastructure;

namespace Terminal;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMyAppTerminal(this IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        return services
            .AddSingleton(configuration)
            .AddMyApp(configuration)
            .AddCommands(typeof(Program).Assembly)
            .AddSingleton<IUserContext, UserContext>();
    }
}

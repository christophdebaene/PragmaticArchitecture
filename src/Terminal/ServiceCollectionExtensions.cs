using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Terminal.CommandBus;
using Terminal.Infrastructure;
using TodoApp.Domain.Users;
using TodoApp.Infrastructure;

namespace Terminal;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTerminal(this IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        return services
            .AddSingleton(configuration)
            .AddTodoApp(configuration)
            .AddCommands(typeof(Program).Assembly)
            .AddSingleton<IUserContext, UserContext>();
    }
}

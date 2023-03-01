using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Terminal.CommandBus
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services, Assembly assembly)
        {
            foreach (var type in assembly.GetTypes().Where(type => !type.IsAbstract && !type.IsInterface))
                foreach (var typeInterface in type.GetInterfaces())
                    if (typeInterface.IsGenericType && typeInterface.GetGenericTypeDefinition() == typeof(ICommand<>))
                        services.AddTransient(type);

             //services.AddScoped(typeInterface, type);

            return services;
        }
    }
}

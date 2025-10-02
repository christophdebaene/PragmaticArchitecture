using System.Text.Json.Serialization;
using TodoApp.Api.Infrastructure;
using TodoApp.Api.Services;
using TodoApp.Domain.Users;

namespace TodoApp.Api;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddOpenApi()
            .AddScoped<IUserContext, UserContext>();        
    }
}

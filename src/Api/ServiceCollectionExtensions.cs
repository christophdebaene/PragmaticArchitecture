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

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services
            .AddScoped<IUserContext, UserContext>()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return services;
    }
}

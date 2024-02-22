using TodoApp.Api.Services;
using TodoApp.Domain.Users;

namespace TodoApp.Api;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<IUserContext, UserContext>()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return services;
    }
}

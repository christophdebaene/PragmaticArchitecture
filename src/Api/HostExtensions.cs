using Microsoft.EntityFrameworkCore;

namespace TodoApp.Api;
public static class HostExtensions
{
    public static async Task CreateDatabaseAsync<T>(this WebApplication app) where T: DbContext
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<T>();
        await dbContext.Database.EnsureCreatedAsync();        
    }
}

using System.ComponentModel;
using Spectre.Console;
using Terminal.CommandBus;
using TodoApp.Infrastructure.Data;

namespace Terminal.Features.Database;

[DisplayName("Create Database")]
public class ShowTable(ApplicationDbContext context) : Command<NoCommandArguments>
{
    public override async Task ExecuteAsync(NoCommandArguments arguments)
    {
        AnsiConsole.Write(new FigletText("Create Database"));
        await context.Database.EnsureCreatedAsync();
    }
}

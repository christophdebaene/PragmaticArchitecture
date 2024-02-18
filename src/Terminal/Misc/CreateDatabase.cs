using System.ComponentModel;
using Spectre.Console;
using Terminal.CommandBus;
using TodoApp.Infrastructure.Data;

namespace Terminal.Misc;

[DisplayName("Create Database")]
public class CreateDatabase : Command<NoCommandArguments>
{
    private readonly ApplicationDbContext _context;
    public CreateDatabase(ApplicationDbContext context)
    {
        _context = context;
    }
    public override async Task ExecuteAsync(NoCommandArguments arguments)
    {
        AnsiConsole.Write(new FigletText("Create Database"));
        await _context.Database.EnsureCreatedAsync();
    }
}

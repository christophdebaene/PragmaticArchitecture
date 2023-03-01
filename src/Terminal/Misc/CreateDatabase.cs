using System.ComponentModel;
using MyApp.Domain;
using Spectre.Console;
using Terminal.CommandBus;

namespace Terminal.Misc;

[DisplayName("Create Database")]
public class CreateDatabase : Command<NoCommandArguments>
{
    private readonly MyAppContext _context;
    public CreateDatabase(MyAppContext context)
    {
        _context = context;
    }
    public override async Task ExecuteAsync(NoCommandArguments arguments)
    {
        AnsiConsole.Write(new FigletText("Create Database"));
        await _context.Database.EnsureCreatedAsync();
    }
}

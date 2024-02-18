using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using Terminal.CommandBus;
using TodoApp.Infrastructure.Data;

namespace Terminal.Features.Database;

[DisplayName("List Tables")]
public class ListTables(ApplicationDbContext context) : Command<NoCommandArguments>
{
    public override async Task ExecuteAsync(NoCommandArguments arguments)
    {
        AnsiConsole.Write(new FigletText("Tables"));
                
        var tables = context.Database.SqlQuery<string>(
            $"SELECT [TABLE_NAME] AS [Name] FROM INFORMATION_SCHEMA.TABLES WHERE [Table_Type] = 'BASE TABLE'");

        var selectedTable = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose table")
                .PageSize(10)
                .AddChoices(tables));

        AnsiConsole.WriteLine($"Select {selectedTable}");
    }
}

public record SelectedTableArgument(string Name) : ICommandArguments
{    
}

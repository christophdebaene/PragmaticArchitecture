using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using Terminal.CommandBus;
using TodoApp.Infrastructure.Database;

namespace Terminal.Features.Database;

[DisplayName("List Tables")]
public class ListTables(ApplicationDbContext context) : ICommand<NoCommandArguments>
{
    public ValueTask<ICommandArgument> ExecuteAsync(NoCommandArguments arguments)
    {
        AnsiConsole.Write(new FigletText("Tables"));

        var tables = context.Database.SqlQuery<string>(
            $"SELECT [TABLE_NAME] AS [Name] FROM INFORMATION_SCHEMA.TABLES WHERE [Table_Type] = 'BASE TABLE'");

        var selectedTable = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose table")
                .PageSize(10)
                .AddChoices(tables));

        return ValueTask.FromResult<ICommandArgument>(new SelectedTableArgument(selectedTable));
    }
}

public record SelectedTableArgument(string Name) : ICommandArgument
{
}

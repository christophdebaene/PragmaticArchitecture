using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Terminal.CommandBus;

namespace Terminal;
public class CompositeCommandArguments : ICommandArguments
{
    public List<Type> Commands { get; set; }
}
public class CompositeCommand(IServiceProvider serviceProvider) : Command<CompositeCommandArguments>
{    
    public override async Task ExecuteAsync(CompositeCommandArguments arguments)
    {
        var commandType = AnsiConsole.Prompt(
            new SelectionPrompt<Type>()
                .Title("Make your selection")
                .PageSize(10)
                .UseConverter(x => ICommand.Name(x))
                .AddChoices(arguments.Commands));

        using (var scope = serviceProvider.CreateScope())
        {
            var command = scope.ServiceProvider.GetRequiredService(commandType) as ICommand;
            await command!.ExecuteAsync(NoCommandArguments.Value);
        }        
    }
}

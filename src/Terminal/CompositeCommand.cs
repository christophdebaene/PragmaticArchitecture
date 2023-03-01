using Spectre.Console;
using Terminal.CommandBus;
using Microsoft.Extensions.DependencyInjection;

namespace Terminal
{
    public class CompositeCommandArguments : CommandArguments
    {
        public List<Type> Commands { get; set; }
    }
    public class CompositeCommand : Command<CompositeCommandArguments>
    {
        private readonly IServiceProvider _serviceProvider;
        public CompositeCommand(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public override async Task ExecuteAsync(CompositeCommandArguments arguments)
        {
            var commandType = AnsiConsole.Prompt(
                new SelectionPrompt<Type>()
                    .Title("Make your selection")
                    .PageSize(10)
                    .UseConverter(x => ICommand.Name(x))
                    .AddChoices(arguments.Commands));


            using (var scope = _serviceProvider.CreateScope())
            {
                var command = scope.ServiceProvider.GetService(commandType) as ICommand;
                await command!.ExecuteAsync(NoCommandArguments.Value);
            }
                
            /*
            var command = _serviceProvider.GetService(commandType) as ICommand;
            await command!.ExecuteAsync(NoCommandArguments.Value);
            */
        }
    }
}

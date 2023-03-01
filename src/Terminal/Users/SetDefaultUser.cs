using System.ComponentModel;
using MediatR;
using Spectre.Console;
using Terminal.CommandBus;

namespace Terminal.Users
{
    [DisplayName("Set Default User")]
    public class SetDefaultUser : Command<NoCommandArguments>
    {
        private readonly ISender _sender;
        public SetDefaultUser(ISender sender)
        {
            _sender = sender;
        }
        public override async Task ExecuteAsync(NoCommandArguments arguments)
        {
            AnsiConsole.Write(new FigletText("Set Default User"));
        }
    }
}

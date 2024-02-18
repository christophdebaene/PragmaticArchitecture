using System.ComponentModel;
using MediatR;
using Spectre.Console;
using Terminal.CommandBus;

namespace Terminal.Users;

[DisplayName("Set Default User")]
public class SetDefaultUser(ISender sender) : Command<NoCommandArguments>
{    
    public override async Task ExecuteAsync(NoCommandArguments arguments)
    {
        AnsiConsole.Write(new FigletText("Set Default User"));
    }
}

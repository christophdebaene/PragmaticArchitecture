using System.ComponentModel;
using MediatR;
using Spectre.Console;
using Terminal.CommandBus;

namespace Terminal.Features.Users;

[DisplayName("Add User")]
public class AddUser(ISender sender) : Command<NoCommandArguments>
{
    public override async Task ExecuteAsync(NoCommandArguments arguments)
    {
        AnsiConsole.Write(new FigletText("Add User"));

        var firstName = AnsiConsole.Ask<string>("First name:");
        var lastName = AnsiConsole.Ask<string>("Last name:");
        /*
        var role = AnsiConsole.Prompt(
             new SelectionPrompt<string>()
                 .Title("Role:")
                 .AddChoices(Enum.GetNames(typeof(Role))));
        */

        await sender.Send(new TodoApp.Application.Features.Users.AddUser
        {
            UserId = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName
        });
    }
}

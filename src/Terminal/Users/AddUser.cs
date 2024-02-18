using System.ComponentModel;
using MediatR;
using Spectre.Console;
using Terminal.CommandBus;

namespace Terminal.Users;

[DisplayName("Add User")]
public class AddUser(ISender sender) : Command<CommandArguments>
{ 
    public override async Task ExecuteAsync(CommandArguments arguments)
    {
        AnsiConsole.Write(new FigletText("Add User"));

        var firstName = AnsiConsole.Ask<string>("Firstname:");
        var lastName = AnsiConsole.Ask<string>("Lastname:");
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

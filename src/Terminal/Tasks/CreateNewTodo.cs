using System.ComponentModel;
using MediatR;
using Terminal.CommandBus;

namespace Terminal.Tasks;

[DisplayName("Create New Todo")]
public class CreateNewTodo : Command<NoCommandArguments>
{
    private readonly ISender _sender;
    public CreateNewTodo(ISender sender)
    {
        _sender = sender;
    }
    public override async Task ExecuteAsync(NoCommandArguments arguments)
    {
    }
}

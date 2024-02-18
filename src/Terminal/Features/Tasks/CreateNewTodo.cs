using System.ComponentModel;
using MediatR;
using Terminal.CommandBus;

namespace Terminal.Features.Tasks;

[DisplayName("Create New Todo")]
public class CreateNewTodo(ISender sender) : Command<NoCommandArguments>
{
    public override async Task ExecuteAsync(NoCommandArguments arguments)
    {
    }
}

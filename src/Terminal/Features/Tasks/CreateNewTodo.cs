using System.ComponentModel;
using Terminal.CommandBus;

namespace Terminal.Features.Tasks;

[DisplayName("Create New Todo")]
public class CreateNewTodo(Mediator.ISender sender) : ICommand<NoCommandArguments>
{
    public ValueTask<ICommandArgument> ExecuteAsync(NoCommandArguments arguments)
    {
        return ValueTask.FromResult<ICommandArgument>(NoCommandArguments.Value);
    }
}

using Ardalis.Result;
using Bricks;
using Mediator;

namespace TodoApp.Application.Features.Tasks;

public record DecreasePriority(Guid TaskId) : ICommand<Result>
{
}
public class DecreasePriorityHandler(IApplicationDbContext context) : ICommandHandler<DecreasePriority, Result>
{
    public async ValueTask<Result> Handle(DecreasePriority command, CancellationToken cancellationToken)
    {
        var todo = await context.Tasks.FindAsync([command.TaskId], cancellationToken: cancellationToken);
        if (todo is null)
        {
            return Result.NotFound();
        }

        return todo.DecreasePriority();
    }
}

using Ardalis.Result;
using Bricks;
using Mediator;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record CompleteTaskItem(Guid TaskId) : IRequest<Result>
{
}
public class CompleteTaskItemHandler(IApplicationDbContext context) : IRequestHandler<CompleteTaskItem, Result>
{
    public async ValueTask<Result> Handle(CompleteTaskItem command, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync([command.TaskId], cancellationToken);
        if (task is null)
        {
            return Result.NotFound();
        }

        return task.Complete();
    }
}

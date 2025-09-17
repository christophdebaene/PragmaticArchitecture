using Ardalis.Result;
using Bricks;
using Mediator;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record IncreasePriority(Guid TaskId) : IRequest<Result>
{
}
public class IncreasePriorityHandler(IApplicationDbContext context) : IRequestHandler<IncreasePriority, Result>
{
    public async ValueTask<Result> Handle(IncreasePriority command, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync([command.TaskId], cancellationToken);
        if (task is null)
        {
            return Result.NotFound();
        }

        return task.IncreasePriority();
    }
}

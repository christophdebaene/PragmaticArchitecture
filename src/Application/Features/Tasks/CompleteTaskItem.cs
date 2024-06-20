using Ardalis.Result;
using Bricks;
using MediatR;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record CompleteTaskItem(Guid TaskId) : IRequest<Result>
{
}
public class CompleteTaskItemHandler(IApplicationDbContext context) : IRequestHandler<CompleteTaskItem, Result>
{
    public async Task<Result> Handle(CompleteTaskItem command, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync([command.TaskId], cancellationToken);
        if (task is null)
        {
            return Result.NotFound();
        }

        return task.Complete();
    }
}

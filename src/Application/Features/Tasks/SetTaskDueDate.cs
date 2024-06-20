using Ardalis.Result;
using Bricks;
using MediatR;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record SetTaskDueDate(Guid TaskId, DateTime DueDate) : IRequest<Result>
{
}
public class SetTaskDueDateHandler(IApplicationDbContext context) : IRequestHandler<SetTaskDueDate, Result>
{
    public async Task<Result> Handle(SetTaskDueDate command, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync([command.TaskId], cancellationToken);
        if (task is null)
        {
            return Result.NotFound();
        }

        return task.SetDueDate(command.DueDate);
    }
}

using Ardalis.Result;
using Bricks;
using Mediator;

namespace TodoApp.Application.Features.Tasks;
public record SetTaskDueDate(Guid TaskId, DateTime DueDate) : ICommand<Result>
{
}
public class SetTaskDueDateHandler(IApplicationDbContext context) : ICommandHandler<SetTaskDueDate, Result>
{
    public async ValueTask<Result> Handle(SetTaskDueDate command, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync([command.TaskId], cancellationToken);
        if (task is null)
        {
            return Result.NotFound();
        }

        return task.SetDueDate(command.DueDate);
    }
}

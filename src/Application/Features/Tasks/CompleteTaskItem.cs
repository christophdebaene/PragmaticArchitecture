using Ardalis.Result;
using Mediator;

namespace TodoApp.Application.Features.Tasks;
public record CompleteTaskItem(Guid TaskId) : ICommand<Result>
{
}
public class CompleteTaskItemHandler(IApplicationDbContext context) : ICommandHandler<CompleteTaskItem, Result>
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

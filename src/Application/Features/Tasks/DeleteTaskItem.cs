using Ardalis.Result;
using Bricks;
using Mediator;

namespace TodoApp.Application.Features.Tasks;

public record DeleteTaskItem(Guid TaskItem) : ICommand<Result>
{
}
public class DeleteTaskHandler(IApplicationDbContext context) : ICommandHandler<DeleteTaskItem, Result>
{
    public async ValueTask<Result> Handle(DeleteTaskItem command, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync([command.TaskItem], cancellationToken);
        if (task is null)
        {
            return Result.NotFound();
        }

        context.Tasks.Remove(task);
        return Result.Success();
    }
}

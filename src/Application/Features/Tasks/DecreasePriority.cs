using Ardalis.Result;
using Bricks;
using MediatR;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record DecreasePriority(Guid TaskId) : IRequest<Result>
{
}
public class DecreasePriorityHandler(IApplicationDbContext context) : IRequestHandler<DecreasePriority, Result>
{
    public async Task<Result> Handle(DecreasePriority command, CancellationToken cancellationToken)
    {
        var todo = await context.Tasks.FindAsync([command.TaskId], cancellationToken: cancellationToken);
        if (todo is null)
        {
            return Result.NotFound();
        }

        return todo.DecreasePriority();
    }
}

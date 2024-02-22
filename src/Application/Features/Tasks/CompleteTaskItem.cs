using Bricks;
using MediatR;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record CompleteTaskItem(Guid TaskId) : IRequest
{
}
public class CompleteTaskItemHandler(IApplicationDbContext context) : IRequestHandler<CompleteTaskItem>
{
    public async Task Handle(CompleteTaskItem command, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync([command.TaskId], cancellationToken);
        task.Complete();        
    }
}

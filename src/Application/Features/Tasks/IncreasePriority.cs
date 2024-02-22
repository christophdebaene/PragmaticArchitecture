using Bricks;
using MediatR;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record IncreasePriority(Guid TaskId) : IRequest
{    
}
public class IncreasePriorityHandler(IApplicationDbContext context) : IRequestHandler<IncreasePriority>
{
    public async Task Handle(IncreasePriority command, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync([command.TaskId], cancellationToken);
        task.IncreasePriority();        
    }
}

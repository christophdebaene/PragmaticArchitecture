using Bricks;
using MediatR;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record SetTaskDueDate(Guid TaskId, DateTime DueDate) : IRequest
{    
}
public class SetTaskDueDateHandler(IApplicationDbContext context) : IRequestHandler<SetTaskDueDate>
{
    public async Task Handle(SetTaskDueDate command, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync([command.TaskId], cancellationToken);
        task.SetDueDate(command.DueDate);
    }
}

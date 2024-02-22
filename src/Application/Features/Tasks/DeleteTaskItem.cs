using Bricks;
using MediatR;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record DeleteTaskItem(Guid TaskItem) : IRequest
{
}
public class DeleteTaskHandler(IApplicationDbContext context) : IRequestHandler<DeleteTaskItem>
{
    public async Task Handle(DeleteTaskItem command, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync([command.TaskItem], cancellationToken);
        context.Tasks.Remove(task);     
    }
}

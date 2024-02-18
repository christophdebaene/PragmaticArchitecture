using Bricks;
using MediatR;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record DecreasePriority : IRequest
{
    public Guid TodoId { get; init; }
}
public class DecreasePriorityHandler(IApplicationDbContext context) : IRequestHandler<DecreasePriority>
{
    public async Task Handle(DecreasePriority command, CancellationToken cancellationToken)
    {
        var todo = await context.Tasks.FindAsync(command.TodoId);
        todo.DecreasePriority();
    }
}

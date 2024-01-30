using Bricks;
using MediatR;
using MyApp.Domain;
using MyApp.Domain.Tasks;

namespace MyApp.Application.Tasks;

[Command]
public record IncreasePriority : IRequest
{
    public Guid TodoId { get; init; }
}
public class IncreasePriorityHandler(MyAppContext context) : IRequestHandler<IncreasePriority>
{    
    public async Task Handle(IncreasePriority command, CancellationToken cancellationToken)
    {
        var task = await context.Set<Todo>().FindAsync(command.TodoId);
        task.IncreasePriority();        
    }
}

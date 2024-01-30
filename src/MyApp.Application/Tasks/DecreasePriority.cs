using Bricks;
using MediatR;
using MyApp.Domain;
using MyApp.Domain.Tasks;

namespace MyApp.Application.Tasks;

[Command]
public record DecreasePriority : IRequest
{
    public Guid TodoId { get; init; }
}

public class DecreasePriorityHandler(MyAppContext context) : IRequestHandler<DecreasePriority>
{    
    public async Task Handle(DecreasePriority command, CancellationToken cancellationToken)
    {
        var todo = await context.Set<Todo>().FindAsync(command.TodoId);
        todo.DecreasePriority();        
    }
}

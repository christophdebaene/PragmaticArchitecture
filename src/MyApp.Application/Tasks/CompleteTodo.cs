using Bricks;
using MediatR;
using MyApp.Domain;
using MyApp.Domain.Tasks;

namespace MyApp.Application.Tasks;

[Command]
public record CompleteTodo : IRequest
{
    public Guid TodoId { get; init; }
}
public class CompleteTodoHandler(MyAppContext context) : IRequestHandler<CompleteTodo>
{    
    public async Task Handle(CompleteTodo command, CancellationToken cancellationToken)
    {
        var todo = await context.Set<Todo>().FindAsync(command.TodoId, cancellationToken);
        todo.Complete();        
    }
}

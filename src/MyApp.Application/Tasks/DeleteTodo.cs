using Bricks;
using MediatR;
using MyApp.Domain;
using MyApp.Domain.Tasks;

namespace MyApp.Application.Tasks;

[Command]
public record DeleteTodo : IRequest
{
    public Guid TodoId { get; init; }
}
public class DeleteTaskHandler(MyAppContext context) : IRequestHandler<DeleteTodo>
{    
    public async Task Handle(DeleteTodo command, CancellationToken cancellationToken)
    {
        var todo = await context.Set<Todo>().FindAsync(command.TodoId);
        context.Set<Todo>().Remove(todo);        
    }
}

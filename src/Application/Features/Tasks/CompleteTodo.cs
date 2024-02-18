using Bricks;
using MediatR;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record CompleteTodo : IRequest
{
    public Guid TodoId { get; init; }
}
public class CompleteTodoHandler(IApplicationDbContext context) : IRequestHandler<CompleteTodo>
{
    public async Task Handle(CompleteTodo command, CancellationToken cancellationToken)
    {
        var todo = await context.Tasks.FindAsync(command.TodoId, cancellationToken);
        todo.Complete();
    }
}

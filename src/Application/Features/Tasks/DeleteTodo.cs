using Bricks;
using MediatR;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record DeleteTodo : IRequest
{
    public Guid TodoId { get; init; }
}
public class DeleteTaskHandler(IApplicationDbContext context) : IRequestHandler<DeleteTodo>
{
    public async Task Handle(DeleteTodo command, CancellationToken cancellationToken)
    {
        var todo = await context.Tasks.FindAsync(command.TodoId);
        context.Tasks.Remove(todo);
    }
}

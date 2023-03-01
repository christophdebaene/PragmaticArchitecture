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
public class DeleteTaskHandler : IRequestHandler<DeleteTodo>
{
    private readonly MyAppContext _context;
    public DeleteTaskHandler(MyAppContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task Handle(DeleteTodo command, CancellationToken cancellationToken)
    {
        var todo = await _context.Set<Todo>().FindAsync(command.TodoId);
        _context.Set<Todo>().Remove(todo);

        return;
    }
}

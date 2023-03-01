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
public class CompleteTodoHandler : IRequestHandler<CompleteTodo>
{
    private readonly MyAppContext _context;
    public CompleteTodoHandler(MyAppContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task Handle(CompleteTodo command, CancellationToken cancellationToken)
    {
        var todo = await _context.Set<Todo>().FindAsync(command.TodoId, cancellationToken);
        todo.Complete();

        return;
    }
}

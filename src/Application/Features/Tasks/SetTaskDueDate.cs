using Bricks;
using MediatR;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record SetTaskDueDate : IRequest
{
    public Guid TodoId { get; init; }
    public DateTime DueDate { get; init; }
}
public class SetTaskDueDateHandler(IApplicationDbContext context) : IRequestHandler<SetTaskDueDate>
{
    public async Task Handle(SetTaskDueDate command, CancellationToken cancellationToken)
    {
        var todo = await context.Tasks.FindAsync(command.TodoId, cancellationToken);
        todo.SetDueDate(command.DueDate);
    }
}

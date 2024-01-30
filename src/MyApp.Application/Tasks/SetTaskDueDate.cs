using Bricks;
using MediatR;
using MyApp.Domain;
using MyApp.Domain.Tasks;

namespace MyApp.Application.Tasks;

[Command]
public record SetTaskDueDate : IRequest
{
    public Guid TodoId { get; init; }
    public DateTime DueDate { get; init; }
}
public class SetTaskDueDateHandler(MyAppContext context) : IRequestHandler<SetTaskDueDate>
{    
    public async Task Handle(SetTaskDueDate command, CancellationToken cancellationToken)
    {
        var todo = await context.Set<Todo>().FindAsync(command.TodoId);
        todo.SetDueDate(command.DueDate);        
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using Bricks;
using MediatR;
using MyApp.Domain;
using MyApp.Domain.Tasks;

namespace MyApp.Application.Tasks
{
    [Command]
    public record SetTaskDueDate : IRequest<Unit>
    {
        public Guid TodoId { get; init; }
        public DateTime DueDate { get; init; }
    }
    public class SetTaskDueDateHandler : IRequestHandler<SetTaskDueDate, Unit>
    {
        private readonly MyAppContext _context;
        public SetTaskDueDateHandler(MyAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Unit> Handle(SetTaskDueDate command, CancellationToken cancellationToken)
        {
            var todo = await _context.Set<Todo>().FindAsync(command.TodoId);
            todo.SetDueDate(command.DueDate);

            return Unit.Value;
        }
    }
}

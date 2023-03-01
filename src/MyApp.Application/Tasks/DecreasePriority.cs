using Bricks;
using MediatR;
using MyApp.Domain;
using MyApp.Domain.Tasks;

namespace MyApp.Application.Tasks
{
    [Command]
    public record DecreasePriority : IRequest
    {
        public Guid TodoId { get; init; }
    }

    public class DecreasePriorityHandler : IRequestHandler<DecreasePriority>
    {
        private readonly MyAppContext _context;
        public DecreasePriorityHandler(MyAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task Handle(DecreasePriority command, CancellationToken cancellationToken)
        {
            var todo = await _context.Set<Todo>().FindAsync(command.TodoId);
            todo.DecreasePriority();

            return;
        }
    }
}

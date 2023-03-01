using Bricks;
using MediatR;
using MyApp.Domain;
using MyApp.Domain.Tasks;

namespace MyApp.Application.Tasks
{
    [Command]
    public record IncreasePriority : IRequest
    {
        public Guid TodoId { get; init; }
    }
    public class IncreasePriorityHandler : IRequestHandler<IncreasePriority>
    {
        private readonly MyAppContext _context;
        public IncreasePriorityHandler(MyAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task Handle(IncreasePriority command, CancellationToken cancellationToken)
        {
            var task = await _context.Set<Todo>().FindAsync(command.TodoId);
            task.IncreasePriority();

            return;
        }
    }
}

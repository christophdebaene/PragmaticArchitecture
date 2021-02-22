using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyApp.Domain;
using MyApp.Domain.Tasks;

namespace MyApp.Application.Tasks
{
    public record IncreasePriority : IRequest<Unit>
    {
        public Guid TodoId { get; init; }
    }
    public class IncreasePriorityHandler : IRequestHandler<IncreasePriority, Unit>
    {
        private readonly MyAppContext _context;
        public IncreasePriorityHandler(MyAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Unit> Handle(IncreasePriority command, CancellationToken cancellationToken)
        {
            var task = await _context.Set<Todo>().FindAsync(command.TodoId);
            task.IncreasePriority();

            return Unit.Value;
        }
    }
}

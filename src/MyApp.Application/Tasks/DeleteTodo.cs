using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyApp.Domain;
using MyApp.Domain.Tasks;

namespace MyApp.Application.Tasks
{
    public record DeleteTodo : IRequest<Unit>
    {
        public Guid TodoId { get; init; }
    }
    public class DeleteTaskHandler : IRequestHandler<DeleteTodo, Unit>
    {
        private readonly MyAppContext _context;
        public DeleteTaskHandler(MyAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Unit> Handle(DeleteTodo command, CancellationToken cancellationToken)
        {
            var todo = await _context.Set<Todo>().FindAsync(command.TodoId);
            _context.Set<Todo>().Remove(todo);

            return Unit.Value;
        }
    }
}

using MediatR;
using MyApp.Domain.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Todos
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
            var todo = await _context.Todo.FindAsync(command.TodoId);
            _context.Todo.Remove(todo);

            return Unit.Value;
        }
    }
}
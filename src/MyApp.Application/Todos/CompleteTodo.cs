using MediatR;
using MyApp.Domain.Model;
using MyApp.Domain.Todos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Todos
{
    public record CompleteTodo : IRequest<Unit>
    {
        public Guid TodoId { get; init; }        
    }
    public class CompleteTodoHandler :IRequestHandler<CompleteTodo, Unit>        
    {        
        private readonly MyAppContext _context;
        public CompleteTodoHandler(MyAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));            
        }        
        public async Task<Unit> Handle(CompleteTodo command, CancellationToken cancellationToken)
        {
            var todo = await _context.Set<Todo>().FindAsync(command.TodoId);
            todo.Complete();

            return Unit.Value;
        }        
    }
}
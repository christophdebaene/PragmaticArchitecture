using FluentValidation;
using MediatR;
using MyApp.Domain.Model;
using MyApp.Domain.Todos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Todos
{
    public record CreateNewTask : IRequest<Unit>
    {
        public Guid TodoId { get; init; }
        public string Title { get; init; }
    }
    public class CreateNewTaskValidator : AbstractValidator<CreateNewTask>
    {
        public CreateNewTaskValidator()
        {
            RuleFor(x => x.TodoId).NotEmpty();
            RuleFor(x => x.Title).Length(1, 255);
        }
    }
    public class CreateNewTaskHandler : IRequestHandler<CreateNewTask, Unit>
    {        
        private readonly MyAppContext _context;
        public CreateNewTaskHandler(MyAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));            
        }
        public async Task<Unit> Handle(CreateNewTask command, CancellationToken cancellationToken)
        {
            var task = new Todo(command.TodoId, command.Title);

            await _context.AddAsync(task);
            return Unit.Value;
        }
    }
}
using Bricks;
using FluentValidation;
using MediatR;
using MyApp.Domain;
using MyApp.Domain.Tasks;

namespace MyApp.Application.Tasks
{
    [Command]
    public record CreateNewTodo : IRequest
    {
        public Guid TodoId { get; init; }
        public string Title { get; init; }
    }
    public class CreateNewTaskValidator : AbstractValidator<CreateNewTodo>
    {
        public CreateNewTaskValidator()
        {
            RuleFor(x => x.TodoId).NotEmpty();
            RuleFor(x => x.Title).Length(1, 255);
        }
    }
    public class CreateNewTaskHandler : IRequestHandler<CreateNewTodo>
    {
        private readonly MyAppContext _context;
        public CreateNewTaskHandler(MyAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task Handle(CreateNewTodo command, CancellationToken cancellationToken)
        {
            var task = new Todo(command.TodoId, command.Title);
            await _context.AddAsync(task);

            return;
        }
    }
}

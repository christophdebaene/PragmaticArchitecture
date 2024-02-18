using Bricks;
using FluentValidation;
using MediatR;
using TodoApp.Domain.Tasks;

namespace TodoApp.Application.Features.Tasks;

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
public class CreateNewTaskHandler(IApplicationDbContext context) : IRequestHandler<CreateNewTodo>
{
    public async Task Handle(CreateNewTodo command, CancellationToken cancellationToken)
    {
        var task = new TodoItem(command.TodoId, command.Title);
        await context.Tasks.AddAsync(task);
    }
}

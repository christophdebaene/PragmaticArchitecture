using Bricks;
using FluentValidation;
using MediatR;
using MyApp.Domain;
using MyApp.Domain.Tasks;

namespace MyApp.Application.Tasks;

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
public class CreateNewTaskHandler(MyAppContext context) : IRequestHandler<CreateNewTodo>
{    
    public async Task Handle(CreateNewTodo command, CancellationToken cancellationToken)
    {
        var task = new Todo(command.TodoId, command.Title);
        await context.AddAsync(task, cancellationToken);        
    }
}

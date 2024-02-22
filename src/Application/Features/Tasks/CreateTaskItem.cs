using Bricks;
using FluentValidation;
using MediatR;
using TodoApp.Domain.Tasks;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record CreateTaskItem(Guid TaskId, string Title) : IRequest
{    
}
public class CreateTaskItemValidator : AbstractValidator<CreateTaskItem>
{
    public CreateTaskItemValidator()
    {
        RuleFor(x => x.TaskId).NotEmpty();
        RuleFor(x => x.Title).Length(1, 255);
    }
}
public class CreateTaskItemHandler(IApplicationDbContext context) : IRequestHandler<CreateTaskItem>
{
    public async Task Handle(CreateTaskItem command, CancellationToken cancellationToken)
    {
        var task = new TaskItem(command.TaskId, command.Title);
        await context.Tasks.AddAsync(task, cancellationToken);        
    }
}

using Ardalis.Result;
using Bricks;
using FluentValidation;
using MediatR;
using TodoApp.Domain.Tasks;

namespace TodoApp.Application.Features.Tasks;

[Command]
public record CreateTaskItem(Guid TaskId, string Title) : IRequest<Result>
{
}
public class CreateTaskItemValidator : AbstractValidator<CreateTaskItem>
{
    public CreateTaskItemValidator()
    {
        RuleFor(x => x.TaskId).NotEmpty();
        RuleFor(x => x.Title).Length(1, 20);
    }
}
public class CreateTaskItemHandler(IApplicationDbContext context) : IRequestHandler<CreateTaskItem, Result>
{
    public async Task<Result> Handle(CreateTaskItem command, CancellationToken cancellationToken)
    {
        var task = new TaskItem(command.TaskId, command.Title);
        await context.Tasks.AddAsync(task, cancellationToken);

        return Result.Success();
    }
}

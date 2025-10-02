using Ardalis.Result;
using FluentValidation;
using Mediator;
using TodoApp.Domain.Tasks;

namespace TodoApp.Application.Features.Tasks;
public record CreateTaskItem(Guid TaskId, string Title) : ICommand<Result>
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
public class CreateTaskItemHandler(IApplicationDbContext context) : ICommandHandler<CreateTaskItem, Result>
{
    public async ValueTask<Result> Handle(CreateTaskItem command, CancellationToken cancellationToken)
    {
        var task = new TaskItem(command.TaskId, command.Title);
        await context.Tasks.AddAsync(task, cancellationToken);

        return Result.Success();
    }
}

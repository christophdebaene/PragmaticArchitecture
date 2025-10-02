using Ardalis.Result;
using Mediator;
using TodoApp.Domain.Tasks;
using TodoApp.Domain.Users;

namespace TodoApp.Application.Features.Tasks;
public record GetTaskDetail(Guid TaskId) : IQuery<Result<TaskDetailModel>>
{
}
public record TaskDetailModel(Guid Id, string Title, TaskPriority Priority, bool IsCompleted)
{
}
public class GetTodoDetailHandler(IApplicationDbContext context, IUserContext userContext) : IQueryHandler<GetTaskDetail, Result<TaskDetailModel>>
{
    public async ValueTask<Result<TaskDetailModel>> Handle(GetTaskDetail request, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync([request.TaskId], cancellationToken);
        if (task is null)
        {
            return Result.NotFound();
        }

        return Result.Success(new TaskDetailModel(task.Id, task.Title, task.Priority, task.IsCompleted));
    }
}

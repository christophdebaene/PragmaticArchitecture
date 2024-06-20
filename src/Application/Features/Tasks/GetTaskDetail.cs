using Ardalis.Result;
using Bricks;
using MediatR;
using TodoApp.Domain.Tasks;
using TodoApp.Domain.Users;

namespace TodoApp.Application.Features.Tasks;

[Query]
public record GetTaskDetail(Guid TaskId) : IRequest<Result<TaskDetailModel>>
{
}
public record TaskDetailModel(Guid Id, string Title, TaskPriority Priority, bool IsCompleted)
{
}
public class GetTodoDetailHandler(IApplicationDbContext context, IUserContext userContext) : IRequestHandler<GetTaskDetail, Result<TaskDetailModel>>
{
    public async Task<Result<TaskDetailModel>> Handle(GetTaskDetail request, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync([request.TaskId], cancellationToken);
        if (task is null)
        {
            return Result.NotFound();
        }

        return Result.Success(new TaskDetailModel(task.Id, task.Title, task.Priority, task.IsCompleted));
    }
}

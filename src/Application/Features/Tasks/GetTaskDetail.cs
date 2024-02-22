using Bricks;
using MediatR;
using TodoApp.Domain.Tasks;
using TodoApp.Domain.Users;

namespace TodoApp.Application.Features.Tasks;

[Query]
public record GetTaskDetail(Guid TaskId) : IRequest<TaskDetailModel>
{    
}
public record TaskDetailModel(Guid Id, string Title, TaskPriority Priority, bool IsCompleted)
{    
}
public class GetTodoDetailHandler(IApplicationDbContext context, IUserContext userContext) : IRequestHandler<GetTaskDetail, TaskDetailModel>
{
    public async Task<TaskDetailModel> Handle(GetTaskDetail request, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync([request.TaskId], cancellationToken);
        return new TaskDetailModel(task.Id, task.Title, task.Priority, task.IsCompleted);
    }
}

using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Tasks;
using TodoApp.Domain.Users;

namespace TodoApp.Application.Features.Tasks;
public record GetTasks : IQuery<Result<IReadOnlyList<TaskHeader>>>
{
}
public record TaskHeader(Guid Id, string Title, TaskPriority Priority);

public class GetTasksHandler(IApplicationDbContext context, IUserContext userContext) : IQueryHandler<GetTasks, Result<IReadOnlyList<TaskHeader>>>
{
    public async ValueTask<Result<IReadOnlyList<TaskHeader>>> Handle(GetTasks query, CancellationToken cancellationToken)
    {
        var tasks = await context.Tasks
           .AsNoTracking()
           .OrderByDescending(x => x.Audit.Created)
           .Where(x => x.Audit.CreatedBy == userContext.CurrentUser.Id)
           .Select(x => new TaskHeader(x.Id, x.Title, x.Priority))
           .ToListAsync(cancellationToken);

        return Result.Success<IReadOnlyList<TaskHeader>>(tasks);
    }
}

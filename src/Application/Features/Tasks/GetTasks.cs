using Bricks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Tasks;
using TodoApp.Domain.Users;

namespace TodoApp.Application.Features.Tasks;

[Query]
public record GetTasks : IRequest<IReadOnlyList<TaskHeader>>
{
}
public record TaskHeader(Guid Id, string Title, TaskPriority Priority);

public class GetTasksHandler(IApplicationDbContext context, IUserContext userContext) : IRequestHandler<GetTasks, IReadOnlyList<TaskHeader>>
{
    public async Task<IReadOnlyList<TaskHeader>> Handle(GetTasks query, CancellationToken cancellationToken)
    {
        return await context.Tasks
           .AsNoTracking()
           .OrderByDescending(x => x.Audit.Created)
           .Where(x => x.Audit.CreatedBy == userContext.CurrentUser.Id)
           .Select(x => new TaskHeader(x.Id, x.Title, x.Priority))           
           .ToListAsync(cancellationToken);
    }
}

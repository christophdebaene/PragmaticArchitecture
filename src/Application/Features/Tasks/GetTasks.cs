using Bricks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Users;

namespace TodoApp.Application.Features.Tasks;

[Query]
public record GetTasks : IRequest<IReadOnlyList<TodoDetailModel>>
{
}
public class GetTasksHandler(IApplicationDbContext context, IUserContext userContext) : IRequestHandler<GetTasks, IReadOnlyList<TodoDetailModel>>
{
    public async Task<IReadOnlyList<TodoDetailModel>> Handle(GetTasks query, CancellationToken cancellationToken)
    {
        return await context.Tasks
           .AsNoTracking()
           .OrderByDescending(x => x.Audit.Created)
           .Where(x => x.Audit.CreatedBy == userContext.CurrentUser.Id)
           .Select(x => new TodoDetailModel
           {
               Id = x.Id,
               Title = x.Title,
               Priority = x.Priority.ToString()
           })
           .ToListAsync(cancellationToken);
    }
}

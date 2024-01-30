using Bricks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain;
using MyApp.Domain.Tasks;
using MyApp.Domain.Users;

namespace MyApp.Application.Tasks;

[Query]
public record GetTasks : IRequest<IReadOnlyList<TodoDetailModel>>
{
}
public class GetTasksHandler(MyAppContext context, IUserContext userContext) : IRequestHandler<GetTasks, IReadOnlyList<TodoDetailModel>>
{
    public async Task<IReadOnlyList<TodoDetailModel>> Handle(GetTasks query, CancellationToken cancellationToken)
    {
        return await context.Set<Todo>()
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

using Bricks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Users;

namespace TodoApp.Application.Features.Tasks;

[Query]
public record GetTodoDetail : IRequest<TodoDetailModel>
{
    public Guid TodoId { get; init; }
}
public class TodoDetailModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Priority { get; set; }
}
public class GetTodoDetailHandler(IApplicationDbContext context, IUserContext userContext) : IRequestHandler<GetTodoDetail, TodoDetailModel>
{
    public async Task<TodoDetailModel> Handle(GetTodoDetail query, CancellationToken cancellationToken)
    {
        return await context.Tasks
          .Where(x => x.Audit.CreatedBy == userContext.CurrentUser.Id)
          .AsNoTracking()
          .Select(x => new TodoDetailModel
          {
              Id = x.Id,
              Title = x.Title,
              Priority = x.Priority.ToString()
          })
          .SingleOrDefaultAsync(x => x.Id == query.TodoId);
    }
}

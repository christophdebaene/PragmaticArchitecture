using Bricks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain;
using MyApp.Domain.Tasks;
using MyApp.Domain.Users;

namespace MyApp.Application.Tasks;

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
public class GetTodoDetailHandler(MyAppContext context, IUserContext userContext) : IRequestHandler<GetTodoDetail, TodoDetailModel>
{
    public async Task<TodoDetailModel> Handle(GetTodoDetail query, CancellationToken cancellationToken)
    {
        return await context.Set<Todo>()
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

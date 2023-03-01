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
public class GetTodoDetailHandler : IRequestHandler<GetTodoDetail, TodoDetailModel>
{
    private readonly MyAppContext _context;
    private readonly IUserContext _userContext;
    public GetTodoDetailHandler(MyAppContext context, IUserContext userContext)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
    }
    public async Task<TodoDetailModel> Handle(GetTodoDetail query, CancellationToken cancellationToken)
    {
        return await _context.Set<Todo>()
          .Where(x => x.Audit.CreatedBy == _userContext.CurrentUser.Id)
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

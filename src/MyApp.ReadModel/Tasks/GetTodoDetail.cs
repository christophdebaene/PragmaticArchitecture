using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bricks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain;
using MyApp.Domain.Tasks;
using MyApp.Domain.Users;

namespace MyApp.ReadModel.Tasks
{
    [Query]
    public record GetTodoDetail : IRequest<TodoDetailModel>
    {
        public Guid TodoId { get; init; }
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
              .Where(x => x.Audit.CreatedBy == _userContext.CurrentUser.Name)
              .AsNoTracking()
              .Select(x => new TodoDetailModel
              {
                  Id = x.Id,
                  Title = x.Title,
                  Priority = (int)x.Priority
              })
              .SingleOrDefaultAsync(x => x.Id == query.TodoId, cancellationToken);
        }
    }
}

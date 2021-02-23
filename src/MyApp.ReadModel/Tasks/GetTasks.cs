using System;
using System.Collections.Generic;
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
    public record GetTasks : IRequest<IReadOnlyList<TodoDetailModel>>
    {
    }
    public class GetTasksHandler : IRequestHandler<GetTasks, IReadOnlyList<TodoDetailModel>>
    {
        private readonly MyAppContext _context;
        private readonly IUserContext _userContext;
        public GetTasksHandler(MyAppContext context, IUserContext userContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }
        public async Task<IReadOnlyList<TodoDetailModel>> Handle(GetTasks query, CancellationToken cancellationToken)
        {
            return await _context.Set<Todo>()
                .AsNoTracking()
                .OrderByDescending(x => x.Audit.Created)
                .Where(x => x.Audit.CreatedBy == _userContext.CurrentUser.Name)
                .Select(x => new TodoDetailModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Priority = (int)x.Priority
                })
                .ToListAsync(cancellationToken);
        }
    }
}

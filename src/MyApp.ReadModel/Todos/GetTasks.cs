using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Model;
using MyApp.Domain.Todos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.ReadModel.Todos
{
    public record GetTasks : IRequest<IReadOnlyList<TodoDetailModel>>
    {
    }
    public class GetTasksHandler : IRequestHandler<GetTasks, IReadOnlyList<TodoDetailModel>>
    {
        private readonly MyAppContext _context;
        public GetTasksHandler(MyAppContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            _context = context;
        }

        public async Task<IReadOnlyList<TodoDetailModel>> Handle(GetTasks query, CancellationToken cancellationToken)
        {
            return await _context.Set<Todo>()
                .AsNoTracking()
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
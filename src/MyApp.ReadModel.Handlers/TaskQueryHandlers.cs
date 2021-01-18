using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Model;
using MyApp.ReadModel.Model;
using MyApp.ReadModel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.ReadModel.Handlers
{
    public class TaskQueryHandlers :

        IRequestHandler<GetTasks, IReadOnlyList<TodoDetailModel>>,
        IRequestHandler<GetTodoDetail, TodoDetailModel>
    {
        private readonly MyAppContext _context;
        public TaskQueryHandlers(MyAppContext context)
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
                .ToListAsync();
        }

        public async Task<TodoDetailModel> Handle(GetTodoDetail query, CancellationToken cancellationToken)
{
            return await _context.Set<Todo>()
              .AsNoTracking()
              .Select(x => new TodoDetailModel
              {
                  Id = x.Id,
                  Title = x.Title,
                  Priority = (int)x.Priority
              })
              .SingleOrDefaultAsync(x => x.Id == query.TodoId);
        }
    }
}
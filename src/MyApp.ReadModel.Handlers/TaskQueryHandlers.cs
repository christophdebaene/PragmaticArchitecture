using MyApp.Domain.Model;
using MyApp.ReadModel.Model;
using MyApp.ReadModel.Queries;
using SlickBus;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyApp.ReadModel.Handlers
{
    public class TaskQueryHandlers :

        IRequestHandler<GetTasks, IReadOnlyList<TaskDetailModel>>,
        IRequestHandler<GetTaskDetail, TaskDetailModel>
    {
        private readonly IQueryContext _context;

        public TaskQueryHandlers(IQueryContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public IReadOnlyList<TaskDetailModel> Handle(GetTasks query)
        {
            return _context.Set<Task>()
                .AsNoTracking()
                .Select(x => new TaskDetailModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Priority = (int)x.Priority
                })
                .ToList();
        }

        public TaskDetailModel Handle(GetTaskDetail query)
        {
            return _context.Set<Task>()
              .AsNoTracking()
              .Select(x => new TaskDetailModel
              {
                  Id = x.Id,
                  Title = x.Title,
                  Priority = (int)x.Priority
              })
              .SingleOrDefault(x => x.Id == query.TaskId);
        }
    }
}
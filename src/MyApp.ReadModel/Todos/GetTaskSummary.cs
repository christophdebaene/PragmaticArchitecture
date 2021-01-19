using Dapper;
using MediatR;
using MyApp.Domain.Todos;
using MyApp.ReadModel.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.ReadModel.Todos
{
    public record GetTaskSummary : IRequest<TaskSummaryModel>
    {
    }
    public class GetTaskSummaryHandler : IRequestHandler<GetTaskSummary, TaskSummaryModel>
    {
        private readonly IConnectionProvider _connectionProvider;
        public GetTaskSummaryHandler(IConnectionProvider connectionProvider)
        {
            if (connectionProvider == null)
                throw new ArgumentNullException(nameof(connectionProvider));

            _connectionProvider = connectionProvider;
        }

        public async Task<TaskSummaryModel> Handle(GetTaskSummary request, CancellationToken cancellationToken)
        {
            var model = new TaskSummaryModel();

            string sql = @"
                SELECT COUNT(*) FROM Todo WHERE IsCompleted = 1
                SELECT Priority as Priority, COUNT(Priority) as Count FROM Todo WHERE IsCompleted = 0 GROUP BY Priority
                SELECT TOP(5) Id, Title, Priority, DueDate FROM Todo WHERE IsCompleted = 0 ORDER By DueDate";

            using (var multi = await _connectionProvider.GetOpenConnection()
                                    .QueryMultipleAsync(sql, new { highPriorityId = TodoPriority.High }))
            {
                model.CompletedCount = (await multi.ReadAsync<int>()).Single();

                var stats = await multi.ReadAsync();
                var uncompletedCount = stats.Select(x => (int)x.Count).Sum();

                stats.ToList().ForEach(x =>
                {
                    int percentage = (int)x.Count == 0 ? 0 : (int)(((double)x.Count / uncompletedCount) * 100);

                    if (x.Priority == (int)TodoPriority.Low)
                        model.UncompletedLowPercentage = percentage;

                    if (x.Priority == (int)TodoPriority.Medium)
                        model.UncompletedMediumPercentage = percentage;

                    if (x.Priority == (int)TodoPriority.High)
                        model.UncompletedHighPercentage = percentage;
                });

                model.Top5HighPriorityTasks = multi.Read<TaskModel>().ToList();
            }

            return model;
        }
    }
}
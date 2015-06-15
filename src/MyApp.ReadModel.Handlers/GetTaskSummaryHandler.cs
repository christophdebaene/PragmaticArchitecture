using Dapper;
using MyApp.Domain.Model;
using MyApp.ReadModel.Model;
using MyApp.ReadModel.Queries;
using SlickBus;
using System;
using System.Linq;

namespace MyApp.ReadModel.Handlers
{
    public class GetTaskSummaryHandler : IRequestHandler<GetTaskSummary, TaskSummaryModel>
    {
        private readonly IConnectionProvider _connectionProvider;

        public GetTaskSummaryHandler(IConnectionProvider connectionProvider)
        {
            if (connectionProvider == null)
                throw new ArgumentNullException("connectionProvider");

            _connectionProvider = connectionProvider;
        }

        public TaskSummaryModel Handle(GetTaskSummary query)
        {
            var model = new TaskSummaryModel();

            string sql = @"
                SELECT COUNT(*) FROM Task WHERE IsCompleted = 1
                SELECT Priority as Priority, COUNT(Priority) as Count FROM Task WHERE IsCompleted = 0 GROUP BY Priority
                SELECT TOP(5) Id, Title, Priority, DueDate FROM Task WHERE IsCompleted = 0 ORDER By DueDate";

            using (var multi = _connectionProvider.GetOpenConnection()
                                    .QueryMultiple(sql, new { highPriorityId = TaskPriority.High }))
            {
                model.CompletedCount = multi.Read<int>().Single();

                var stats = multi.Read().ToList();
                var uncompletedCount = stats.Select(x => (int)x.Count).Sum();

                stats.ForEach(x =>
                    {
                        int percentage = (int)x.Count == 0 ? 0 : (int)(((double)x.Count / uncompletedCount) * 100);

                        if (x.Priority == (int)TaskPriority.Low)
                            model.UncompletedLowPercentage = percentage;

                        if (x.Priority == (int)TaskPriority.Medium)
                            model.UncompletedMediumPercentage = percentage;

                        if (x.Priority == (int)TaskPriority.High)
                            model.UncompletedHighPercentage = percentage;
                    });

                model.Top5HighPriorityTasks = multi.Read<TaskModel>().ToList();
            }

            return model;
        }
    }
}
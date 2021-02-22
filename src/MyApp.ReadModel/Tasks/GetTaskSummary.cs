using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using MyApp.Domain.Tasks;
using MyApp.Domain.Users;
using MyApp.ReadModel.Infrastructure;

namespace MyApp.ReadModel.Tasks
{
    public record GetTaskSummary : IRequest<TaskSummaryModel>
    {
    }
    public class GetTaskSummaryHandler : IRequestHandler<GetTaskSummary, TaskSummaryModel>
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly IUserContext _userContext;
        public GetTaskSummaryHandler(IConnectionProvider connectionProvider, IUserContext userContext)
        {
            _connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }
        public async Task<TaskSummaryModel> Handle(GetTaskSummary request, CancellationToken cancellationToken)
        {
            var model = new TaskSummaryModel();

            string sql = @"
                SELECT COUNT(*) FROM Todo WHERE IsCompleted = 1 AND Audit_CreatedBy = @UserId
                SELECT Priority as Priority, COUNT(Priority) as Count FROM Todo WHERE IsCompleted = 0 AND Audit_CreatedBy = @UserId GROUP BY Priority
                SELECT TOP(5) Id, Title, Priority, DueDate FROM Todo WHERE IsCompleted = 0 AND Audit_CreatedBy = @UserId ORDER By DueDate";

            using (var multi = await _connectionProvider.GetOpenConnection()
                                    .QueryMultipleAsync(sql, new { UserId = _userContext.CurrentUser.Name }))
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

                model.Top5HighPriorityTasks = (await multi.ReadAsync<TaskModel>()).ToList();
            }

            return model;
        }
    }
}

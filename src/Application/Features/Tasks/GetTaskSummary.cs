using Ardalis.Result;
using Dapper;
using Mediator;
using TodoApp.Domain.Tasks;
using TodoApp.Domain.Users;

namespace TodoApp.Application.Features.Tasks;
public record GetTaskSummary : IQuery<Result<TaskSummaryModel>>
{
}
public record TaskSummaryModel
{
    public int CompletedCount { get; set; }
    public int UncompletedLowPercentage { get; set; }
    public int UncompletedMediumPercentage { get; set; }
    public int UncompletedHighPercentage { get; set; }
    public List<TaskHeader> Top5HighPriorityTasks { get; set; } = [];
}
public class GetTaskSummaryHandler(IDbConnectionFactory dbConnectionFactory, IUserContext userContext) : IQueryHandler<GetTaskSummary, Result<TaskSummaryModel>>
{
    public async ValueTask<Result<TaskSummaryModel>> Handle(GetTaskSummary request, CancellationToken cancellationToken)
    {
        var model = new TaskSummaryModel();
        var sql = @"
        SELECT COUNT(*) FROM [TaskItem] WHERE IsCompleted = 1 AND Audit_CreatedBy = @UserId
        SELECT Priority as Priority, COUNT(Priority) as Count FROM [TaskItem] WHERE IsCompleted = 0 AND Audit_CreatedBy = @UserId GROUP BY Priority
        SELECT TOP(5) Id, Title, Priority FROM [TaskItem] WHERE IsCompleted = 0 AND Audit_CreatedBy = @UserId ORDER By DueDate";

        var connection = dbConnectionFactory.GetConnection();

        using (var multi = await connection.QueryMultipleAsync(sql, new { UserId = userContext.CurrentUser.Id }))
        {
            model.CompletedCount = await multi.ReadSingleAsync<int>();

            var stats = await multi.ReadAsync();
            var uncompletedCount = stats.Select(x => (int)x.Count).Sum();

            foreach (var stat in stats)
            {
                var percentage = (int)stat.Count == 0 ? 0 : (int)((double)stat.Count / uncompletedCount * 100);

                if (stat.Priority == TaskPriority.Low.ToString())
                    model.UncompletedLowPercentage = percentage;
                else if (stat.Priority == TaskPriority.Medium.ToString())
                    model.UncompletedMediumPercentage = percentage;
                else if (stat.Priority == TaskPriority.High.ToString())
                    model.UncompletedHighPercentage = percentage;
            }

            var top5 = await multi.ReadAsync<TaskHeader>();
            model.Top5HighPriorityTasks = top5.ToList();

            return Result.Success(model);
        }
    }
}

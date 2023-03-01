using Bricks;
using Dapper;
using MediatR;
using MyApp.Application.Infrastructure;
using MyApp.Domain.Tasks;
using MyApp.Domain.Users;

namespace MyApp.Application.Tasks;

[Query]
public record GetTaskSummary : IRequest<TaskSummaryModel>
{
}
public class TaskSummaryModel
{
    public int CompletedCount { get; set; }
    public int UncompletedLowPercentage { get; set; }
    public int UncompletedMediumPercentage { get; set; }
    public int UncompletedHighPercentage { get; set; }
    public List<TaskModel> Top5HighPriorityTasks { get; set; } = new List<TaskModel>();
}

public class TaskModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Priority { get; set; }
    public DateTime DueDate { get; set; }
}

public class GetTaskSummaryHandler : IRequestHandler<GetTaskSummary, TaskSummaryModel>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly IUserContext _userContext;
    public GetTaskSummaryHandler(IDbConnectionFactory dbConnectionFactory, IUserContext userContext)
    {
        _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
    }
    public async Task<TaskSummaryModel> Handle(GetTaskSummary request, CancellationToken cancellationToken)
    {
        var model = new TaskSummaryModel();
        var sql = @"
                SELECT COUNT(*) FROM Todo WHERE IsCompleted = 1 AND Audit_CreatedBy = @UserId
                SELECT Priority as Priority, COUNT(Priority) as Count FROM Todo WHERE IsCompleted = 0 AND Audit_CreatedBy = @UserId GROUP BY Priority
                SELECT TOP(5) Id, Title, Priority, DueDate FROM Todo WHERE IsCompleted = 0 AND Audit_CreatedBy = @UserId ORDER By DueDate";

        var connection = _dbConnectionFactory.GetConnection();

        using (var multi = await connection.QueryMultipleAsync(sql, new { UserId = _userContext.CurrentUser.Id }))
        {
            model.CompletedCount = await multi.ReadSingleAsync<int>();

            var stats = await multi.ReadAsync();
            var uncompletedCount = stats.Select(x => (int)x.Count).Sum();

            foreach (var stat in stats)
            {
                int percentage = (int)stat.Count == 0 ? 0 : (int)(((double)stat.Count / uncompletedCount) * 100);

                if (stat.Priority == TodoPriority.Low.ToString())
                    model.UncompletedLowPercentage = percentage;

                if (stat.Priority == TodoPriority.Medium.ToString())
                    model.UncompletedMediumPercentage = percentage;

                if (stat.Priority == TodoPriority.High.ToString())
                    model.UncompletedHighPercentage = percentage;
            }

            var top5 = await multi.ReadAsync<TaskModel>();
            model.Top5HighPriorityTasks = top5.ToList();

            return model;
        }
    }
}

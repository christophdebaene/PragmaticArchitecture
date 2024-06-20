using Ardalis.Result;
using Bricks.Model;
using TodoApp.Domain.Tasks.Events;

namespace TodoApp.Domain.Tasks;
public class TaskItem : BaseEntity, IAuditable
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public bool IsCompleted { get; set; }
    public AuditInfo Audit { get; set; } = new AuditInfo();
    public TaskItem()
    {
    }
    public TaskItem(Guid id, string title)
    {
        Id = id;
        Title = title;
        Priority = TaskPriority.Medium;
        DueDate = SystemClock.GetUtcNow().AddDays(1);
        IsCompleted = false;

        AddDomainEvent(new TaskItemCreated(this));
    }
    public Result Complete()
    {
        if (IsCompleted)
            return Errors.AlreadyCompleted(Id);

        IsCompleted = true;
        AddDomainEvent(new TaskItemCompleted(this));
        return Result.Success();
    }

    public Result IncreasePriority()
    {
        if (IsCompleted)
            return Errors.AlreadyCompleted(Id);

        switch (Priority)
        {
            case TaskPriority.Low:
                Priority = TaskPriority.Medium;
                break;
            case TaskPriority.Medium:
                Priority = TaskPriority.High;
                break;
            case TaskPriority.High:
                return Errors.HighestPriority(Id);
        }

        return Result.Success();
    }

    public Result DecreasePriority()
    {
        if (IsCompleted)
            return Errors.AlreadyCompleted(Id);

        switch (Priority)
        {
            case TaskPriority.Low:
                return Errors.LowestPriority(Id);
            case TaskPriority.Medium:
                Priority = TaskPriority.Low;
                break;
            case TaskPriority.High:
                Priority = TaskPriority.Medium;
                break;
        }

        return Result.Success();
    }
    public Result SetDueDate(DateTime dateTime)
    {
        DueDate = dateTime;
        return Result.Success();
    }
}

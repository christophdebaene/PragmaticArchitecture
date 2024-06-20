using Ardalis.Result;

namespace TodoApp.Domain.Tasks;

public static class Errors
{
    public static Result AlreadyCompleted(Guid id) => Result.Invalid(new ValidationError(id.ToString(), "Task is already completed", "Tasks.Completed", ValidationSeverity.Error));
    public static Result HighestPriority(Guid id) => Result.Invalid(new ValidationError(id.ToString(), "Task has already the highest priority", "Tasks.HighestPriorty", ValidationSeverity.Error));
    public static Result LowestPriority(Guid id) => Result.Invalid(new ValidationError(id.ToString(), "Task has already the lowest priority", "Tasks.LowestPriorty", ValidationSeverity.Error));
}

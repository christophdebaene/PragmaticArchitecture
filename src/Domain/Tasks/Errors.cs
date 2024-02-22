using Ardalis.Result;

namespace TodoApp.Domain.Tasks;

public static class Errors
{
    internal static Result CreateErrorResult(string code, string message, string identifier) => Result.Invalid(new ValidationError(identifier, message, code, ValidationSeverity.Error));
    public static Result AlreadyCompleted(string name) => CreateErrorResult("todoitem.already.completed", $"Todo item '${name}' is already completed", nameof(TaskItem.IsCompleted));
    public static Result HighestPriority() => CreateErrorResult("todoitem.highest.priority", "Task has already the highest priority", nameof(TaskItem.Priority));
    public static Result LowestPriority()=> CreateErrorResult("todoitem.lowest.priority.", "Task has already the lowest priority", nameof(TaskItem.Priority));
}

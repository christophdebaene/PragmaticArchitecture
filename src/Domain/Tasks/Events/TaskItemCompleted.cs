using Bricks.Model;

namespace TodoApp.Domain.Tasks.Events;

public record TaskItemCompleted(TaskItem Item) : IDomainEvent
{    
}

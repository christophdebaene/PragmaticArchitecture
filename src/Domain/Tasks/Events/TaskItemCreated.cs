using Bricks.Model;

namespace TodoApp.Domain.Tasks.Events;

public record TaskItemCreated(TaskItem Item) : IDomainEvent
{    
}

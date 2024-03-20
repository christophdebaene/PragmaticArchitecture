using Bricks.Model;

namespace TodoApp.Domain.Tasks.Events;

public record TaskItemDeleted(TaskItem Item) : IDomainEvent
{    
}

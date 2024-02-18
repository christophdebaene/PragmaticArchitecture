using Bricks.Model;

namespace TodoApp.Domain.Tasks.Events;

public record TodoItemCompleted(TodoItem Item) : BaseEvent
{    
}

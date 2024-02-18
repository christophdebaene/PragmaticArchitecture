using Bricks.Model;

namespace TodoApp.Domain.Tasks.Events;

public record TodoItemCreated(TodoItem Item) : BaseEvent
{    
}

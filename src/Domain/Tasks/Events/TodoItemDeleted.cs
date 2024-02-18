using Bricks.Model;

namespace TodoApp.Domain.Tasks.Events;

public record TodoItemDeleted(TodoItem Item) : BaseEvent
{    
}

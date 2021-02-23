using System;
using System.ComponentModel.DataAnnotations.Schema;
using Bricks.Domain;

namespace MyApp.Domain.Tasks
{
    [Table(nameof(Todo))]
    public class Todo : IAuditable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime? DueDate { get; set; }
        public TodoPriority Priority { get; set; }
        public bool IsCompleted { get; set; }
        public AuditInfo Audit { get; set; } = new AuditInfo();
        public Todo()
        {
        }
        public Todo(Guid id, string title)
        {
            Id = id;
            Title = title;
            Priority = TodoPriority.Medium;
            DueDate = SystemClock.GetUtcNow().AddDays(1);
            IsCompleted = false;
        }
        public void Complete()
        {
            if (IsCompleted)
                throw new InvalidOperationException("Task is already completed");

            IsCompleted = true;
        }

        public void IncreasePriority()
        {
            if (IsCompleted)
                throw new InvalidOperationException("Task is already completed and cannot be changed");

            if (Priority == TodoPriority.Low)
                Priority = TodoPriority.Medium;
            else if (Priority == TodoPriority.Medium)
                Priority = TodoPriority.High;
            else if (Priority == TodoPriority.High)
                throw new InvalidOperationException("Task has already the highest priority");
        }

        public void DecreasePriority()
        {
            if (IsCompleted)
                throw new InvalidOperationException("Task is already completed and cannot be changed");

            if (Priority == TodoPriority.Low)
                throw new InvalidOperationException("Task has already the lowest priority");
            else if (Priority == TodoPriority.Medium)
                Priority = TodoPriority.Low;
            else if (Priority == TodoPriority.High)
                Priority = TodoPriority.Medium;
        }
        public void SetDueDate(DateTime dateTime)
        {
            DueDate = dateTime;
        }
    }
}

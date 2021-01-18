using System;

namespace MyApp.Domain.Model
{    
    public class Todo
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public TodoPriority Priority { get; set; }
        public bool IsCompleted { get; set; }

        public DateTime CreationDate { get; set; }

        public Todo()
        {
        }

        public Todo(Guid id, string title, ISystemClock systemClock)
        {
            Id = id;
            Title = title;

            Priority = TodoPriority.Medium;
            CreationDate = systemClock.UtcNow;
            DueDate = systemClock.UtcNow.AddDays(1);
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
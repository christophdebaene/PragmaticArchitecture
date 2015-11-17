using System;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Model
{
    public class Task
    {
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public TaskPriority Priority { get; set; }
        public bool IsCompleted { get; set; }

        public DateTime CreationDate { get; set; }

        public Task()
        {
        }

        public Task(Guid id, string title, ISystemClock systemClock)
        {
            Id = id;
            Title = title;

            Priority = TaskPriority.Medium;
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

            if (Priority == TaskPriority.Low)
                Priority = TaskPriority.Medium;
            else if (Priority == TaskPriority.Medium)
                Priority = TaskPriority.High;
            else if (Priority == TaskPriority.High)
                throw new InvalidOperationException("Task has already the highest priority");
        }

        public void DecreasePriority()
        {
            if (IsCompleted)
                throw new InvalidOperationException("Task is already completed and cannot be changed");

            if (Priority == TaskPriority.Low)
                throw new InvalidOperationException("Task has already the lowest priority");
            else if (Priority == TaskPriority.Medium)
                Priority = TaskPriority.Low;
            else if (Priority == TaskPriority.High)
                Priority = TaskPriority.Medium;
        }

        public void SetDueDate(DateTime dateTime)
        {
            DueDate = dateTime;
        }
    }
}
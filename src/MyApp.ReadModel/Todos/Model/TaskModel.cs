using System;

namespace MyApp.ReadModel.Todos
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
        public DateTime DueDate { get; set; }
    }
}
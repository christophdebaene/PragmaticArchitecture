using System;

namespace MyApp.ReadModel.Model
{
    public class TaskDetailModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
    }
}
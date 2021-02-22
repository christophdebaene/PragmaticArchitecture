using System;

namespace MyApp.ReadModel.Tasks
{
    public class TodoDetailModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
    }
}

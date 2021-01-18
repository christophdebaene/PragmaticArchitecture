using System.Collections.Generic;

namespace MyApp.ReadModel.Model
{
    public class TaskSummaryModel
    {
        public int CompletedCount { get; set; }
        public int UncompletedLowPercentage { get; set; }
        public int UncompletedMediumPercentage { get; set; }
        public int UncompletedHighPercentage { get; set; }
        public List<TaskModel> Top5HighPriorityTasks { get; set; } = new List<TaskModel>();
    }
}
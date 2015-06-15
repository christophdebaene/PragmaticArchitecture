using MyApp.ReadModel.Model;
using SlickBus;
using System;

namespace MyApp.ReadModel.Queries
{
    public class GetTaskDetail : Request<TaskDetailModel>
    {
        public Guid TaskId { get; private set; }

        public GetTaskDetail(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}
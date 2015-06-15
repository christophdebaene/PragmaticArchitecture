using SlickBus;
using System;

namespace MyApp.Application.Commands
{
    public class CompleteTask : Request<Unit>
    {
        public Guid TaskId { get; private set; }

        public CompleteTask(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}
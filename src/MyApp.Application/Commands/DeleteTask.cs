using SlickBus;
using System;

namespace MyApp.Application.Commands
{
    public class DeleteTask : Request<Unit>
    {
        public Guid TaskId { get; private set; }

        public DeleteTask(Guid id)
        {
            TaskId = id;
        }
    }
}
using SlickBus;
using System;

namespace MyApp.Application.Commands
{
    public class SetTaskDueDate : Request<Unit>
    {
        public Guid TaskId { get; set; }
        public DateTime DueDate { get; set; }
    }
}
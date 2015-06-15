using SlickBus;
using System;

namespace MyApp.Application.Commands
{
    public class CancelTask : Request<Unit>
    {
        public Guid TaskId { get; set; }
    }
}
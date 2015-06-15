using SlickBus;
using System;

namespace MyApp.Application.Commands
{
    public class DecreasePriority : Request<Unit>
    {
        public Guid TaskId { get; set; }
    }
}
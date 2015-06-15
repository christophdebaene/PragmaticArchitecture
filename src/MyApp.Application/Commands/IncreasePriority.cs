using SlickBus;
using System;

namespace MyApp.Application.Commands
{
    public class IncreasePriority : Request<Unit>
    {
        public Guid TaskId { get; set; }
    }
}
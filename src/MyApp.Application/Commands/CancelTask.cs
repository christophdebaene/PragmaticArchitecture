using MediatR;
using System;

namespace MyApp.Application.Commands
{
    public class CancelTask : IRequest<Unit>
    {
        public Guid TaskId { get; set; }
    }
}
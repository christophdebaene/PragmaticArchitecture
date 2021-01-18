using MediatR;
using System;

namespace MyApp.Application.Commands
{
    public record CancelTask : IRequest<Unit>
    {
        public Guid TaskId { get; init; }
    }
}
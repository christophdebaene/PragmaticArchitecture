using MediatR;
using System;

namespace MyApp.Application.Commands
{
    public record SetTaskDueDate : IRequest<Unit>
    {
        public Guid TodoId { get; init; }
        public DateTime DueDate { get; init; }
    }
}
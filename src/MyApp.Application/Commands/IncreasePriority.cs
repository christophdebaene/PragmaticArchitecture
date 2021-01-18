using MediatR;
using System;

namespace MyApp.Application.Commands
{
    public record IncreasePriority : IRequest<Unit>
    {
        public Guid TodoId { get; init; }
    }
}
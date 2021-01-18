using MediatR;
using System;

namespace MyApp.Application.Commands
{
    public record DeleteTask : IRequest<Unit>
    {
        public Guid TodoId { get; init; }
    }
}
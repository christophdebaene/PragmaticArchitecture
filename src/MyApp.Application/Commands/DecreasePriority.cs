using MediatR;
using System;

namespace MyApp.Application.Commands
{
    public record DecreasePriority : IRequest<Unit>
    {
        public Guid TodoId { get; init; }
    }
}
using MediatR;
using System;

namespace MyApp.Application.Commands
{
    public record CompleteTodo : IRequest<Unit>
    {
        public Guid TodoId { get; init; }        
    }
}
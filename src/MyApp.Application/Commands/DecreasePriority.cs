using MediatR;
using System;

namespace MyApp.Application.Commands
{
    public class DecreasePriority : IRequest<Unit>
    {
        public Guid TodoId { get; set; }
    }
}
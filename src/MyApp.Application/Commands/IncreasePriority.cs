using MediatR;
using System;

namespace MyApp.Application.Commands
{
    public class IncreasePriority : IRequest<Unit>
    {
        public Guid TodoId { get; set; }
    }
}
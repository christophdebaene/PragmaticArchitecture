using MediatR;
using System;

namespace MyApp.Application.Commands
{
    public class SetTaskDueDate : IRequest<Unit>
    {
        public Guid TodoId { get; set; }
        public DateTime DueDate { get; set; }
    }
}
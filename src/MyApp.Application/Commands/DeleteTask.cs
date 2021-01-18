using MediatR;
using System;

namespace MyApp.Application.Commands
{
    public class DeleteTask : IRequest<Unit>
    {
        public Guid TodoId { get; }
        public DeleteTask(Guid id)
        {
            TodoId = id;
        }
    }
}
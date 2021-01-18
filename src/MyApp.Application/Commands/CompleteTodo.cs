using MediatR;
using System;

namespace MyApp.Application.Commands
{
    public class CompleteTodo : IRequest<Unit>
    {
        public Guid TodoId { get; }
        public CompleteTodo(Guid taskId)
        {
            TodoId = taskId;
        }
    }
}
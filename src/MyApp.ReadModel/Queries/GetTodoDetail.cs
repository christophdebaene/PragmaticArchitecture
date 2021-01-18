using MediatR;
using MyApp.ReadModel.Model;
using System;

namespace MyApp.ReadModel.Queries
{
    public record GetTodoDetail : IRequest<TodoDetailModel>
    {
        public Guid TodoId { get; init; }        
    }
}
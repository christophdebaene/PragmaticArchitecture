using MediatR;
using MyApp.ReadModel.Model;
using System;

namespace MyApp.ReadModel.Queries
{
    public class GetTodoDetail : IRequest<TodoDetailModel>
    {
        public Guid TodoId { get; }
        public GetTodoDetail(Guid taskId)
        {
            TodoId = taskId;
        }
    }
}
using MediatR;
using MyApp.ReadModel.Model;
using System.Collections.Generic;

namespace MyApp.ReadModel.Queries
{
    public record GetTasks : IRequest<IReadOnlyList<TodoDetailModel>>
    {
    }
}
using MediatR;
using MyApp.ReadModel.Model;

namespace MyApp.ReadModel.Queries
{
    public record GetTaskSummary : IRequest<TaskSummaryModel>
    {
    }
}
using MediatR;
using MyApp.ReadModel.Model;

namespace MyApp.ReadModel.Queries
{
    public class GetTaskSummary : IRequest<TaskSummaryModel>
    {
    }
}
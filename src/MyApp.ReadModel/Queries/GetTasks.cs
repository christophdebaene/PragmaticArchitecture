using MyApp.ReadModel.Model;
using SlickBus;
using System.Collections.Generic;

namespace MyApp.ReadModel.Queries
{
    public class GetTasks : Request<IReadOnlyList<TaskDetailModel>>
    {
    }
}
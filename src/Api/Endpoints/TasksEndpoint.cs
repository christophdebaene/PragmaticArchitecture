using Ardalis.Result.AspNetCore;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Features.Tasks;

namespace Api.Endpoints;
public static class TasksEndpoint
{
    public static IEndpointRouteBuilder MapTasks(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api");

        api.MapGet("/tasks", GetTasks).WithName(nameof(GetTasks));
        api.MapGet("/tasks/summary", GetTaskSummary);
        api.MapGet("/tasks/{id}", GetTaskDetail);
        api.MapPut("/tasks/{id}/complete", CompleteTaskItem);
        api.MapPut("/tasks/{id}/priority/increase", IncreasePriority);
        api.MapPut("/tasks/{id}/priority/decrease", DecreasePriority);
        api.MapDelete("/tasks/{id}", DeleteTaskItem);
        api.MapPost("/tasks", CreateTaskItem);
        return app;
    }
    internal static async Task<IResult> GetTasks(ISender sender)
    {
        var response = await sender.Send(new GetTasks());
        return response.ToMinimalApiResult();
    }
    internal static async Task<IResult> GetTaskSummary(ISender sender)
    {
        var response = await sender.Send(new GetTaskSummary());
        return response.ToMinimalApiResult();
    }
    internal static async Task<IResult> GetTaskDetail(ISender sender, Guid id)
    {
        var response = await sender.Send(new GetTaskDetail(id));
        return response.ToMinimalApiResult();
    }
    internal static async Task<IResult> CompleteTaskItem(ISender sender, Guid id)
    {
        var response = await sender.Send(new CompleteTaskItem(id));
        return response.ToMinimalApiResult();
    }
    internal static async Task<IResult> IncreasePriority(ISender sender, Guid id)
    {
        var response = await sender.Send(new IncreasePriority(id));
        return response.ToMinimalApiResult();
    }
    internal static async Task<IResult> DecreasePriority(ISender sender, Guid id)
    {
        var response = await sender.Send(new DecreasePriority(id));
        return response.ToMinimalApiResult();
    }
    internal static async Task<IResult> DeleteTaskItem(ISender sender, Guid id)
    {
        var response = await sender.Send(new DeleteTaskItem(id));
        return response.ToMinimalApiResult();
    }

    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    internal static async Task<IResult> CreateTaskItem(ISender sender, [FromBody] CreateTaskItem create)
    {
        var response = await sender.Send(create);
        return response.ToMinimalApiResult();
    }
}

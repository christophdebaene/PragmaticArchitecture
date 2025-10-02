using Ardalis.Result.AspNetCore;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Features.Tasks;

namespace Api.Endpoints;
public static class TasksEndpoint
{
    public static IEndpointRouteBuilder MapTasks(this IEndpointRouteBuilder app)
    {
        app.MapGet("/tasks", GetTasks);
        app.MapGet("/tasks/summary", GetTaskSummary);
        app.MapGet("/tasks/{id}", GetTaskDetail);
        app.MapPut("/tasks/{id}/complete", CompleteTaskItem);
        app.MapPut("/tasks/{id}/priority/increase", IncreasePriority);
        app.MapPut("/tasks/{id}/priority/decrease", DecreasePriority);
        app.MapDelete("/tasks/{id}", DeleteTaskItem);
        app.MapPost("/tasks", CreateTaskItem);
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

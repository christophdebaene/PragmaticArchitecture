using Ardalis.Result.AspNetCore;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Features.Tasks;

namespace Api.Endpoints;
public static class TasksEndpoint
{
    public static WebApplication MapTasks(this WebApplication app)
    {
        app.MapGet("/tasks", async (ISender sender) =>
        {
            var response = await sender.Send(new GetTasks());
            return response.ToMinimalApiResult();
        });

        app.MapGet("/tasks/summary", async (ISender sender) =>
        {
            var response = await sender.Send(new GetTaskSummary());
            return response.ToMinimalApiResult();
        });

        app.MapGet("/tasks/{id}", async (ISender sender, Guid id) =>
        {
            var response = await sender.Send(new GetTaskDetail(id));
            return response.ToMinimalApiResult();
        });

        app.MapPut("/tasks/{id}/complete", async (ISender sender, Guid id) =>
        {
            var response = await sender.Send(new CompleteTaskItem(id));
            return response.ToMinimalApiResult();
        });

        app.MapPut("/tasks/{id}/priority/increase", async (ISender sender, Guid id) =>
        {
            var response = await sender.Send(new IncreasePriority(id));
            return response.ToMinimalApiResult();
        });

        app.MapPut("/tasks/{id}/priority/decrease", async (ISender sender, Guid id) =>
        {
            var response = await sender.Send(new DecreasePriority(id));
            return response.ToMinimalApiResult();
        });

        app.MapDelete("/tasks/{id}", async (ISender sender, Guid id) =>
        {
            var response = await sender.Send(new DeleteTaskItem(id));
            return response.ToMinimalApiResult();
        });

        app.MapPost("/tasks", async (ISender sender, [FromBody] CreateTaskItem create) =>
        {
            var response = await sender.Send(create);
            return response.ToMinimalApiResult();
        });

        return app;
    }
}

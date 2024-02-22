using MediatR;
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
            return response;
        });

        app.MapGet("/tasks/summary", async (ISender sender) =>
        {
            var response = await sender.Send(new GetTaskSummary());
            return response;
        });

        app.MapGet("/task/{id}", async (ISender sender, Guid id) =>
        {
            var response = await sender.Send(new GetTaskDetail(id));
            return response;
        });

        app.MapPut("/task/{id}/complete", async (ISender sender, Guid id) =>
        {
            await sender.Send(new CompleteTaskItem(id));
            return Results.Ok();
        });

        app.MapDelete("/task/{id}", async (ISender sender, Guid id) =>
        {
            await sender.Send(new DeleteTaskItem(id));
            return Results.Ok();
        });

        app.MapPost("/tasks", async (ISender sender, [FromBody] CreateTaskItem create) =>
        {
            await sender.Send(create);
        });

        return app;
    }
}

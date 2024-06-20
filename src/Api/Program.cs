using Api.Endpoints;
using TodoApp.Api;
using TodoApp.Api.Extensions;
using TodoApp.Infrastructure;
using TodoApp.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services
    .AddTodoApp(builder.Configuration)
    .AddApi(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler((_ => { }));

await app.CreateDatabaseAsync<ApplicationDbContext>();

app.MapTasks();
app.Run();

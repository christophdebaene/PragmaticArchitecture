using Api.Endpoints;
using TodoApp.Api;
using TodoApp.Infrastructure;
using TodoApp.Infrastructure.Data;

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

await app.CreateDatabaseAsync<ApplicationDbContext>();

app.MapTasks();
app.Run();

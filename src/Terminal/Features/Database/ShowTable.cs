﻿//using System.ComponentModel;
//using Spectre.Console;
//using Terminal.CommandBus;
//using TodoApp.Infrastructure.Data;

//namespace Terminal.Features.Database;

//[DisplayName("Create Database")]
//public class ShowTable(ApplicationDbContext context) : ICommand<NoCommandArguments>
//{
//    public async ValueTask<ICommandArgument> ExecuteAsync(NoCommandArguments arguments)
//    {
//        AnsiConsole.Write(new FigletText("Create Database"));
//        await context.Database.EnsureCreatedAsync();

//        return ValueTask.FromResult<ICommandArgument>(NoCommandArguments.Value);
//    }
//}

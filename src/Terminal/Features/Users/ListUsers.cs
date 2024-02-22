//using System.ComponentModel;
//using MediatR;
//using Spectre.Console;
//using Terminal.CommandBus;

//namespace Terminal.Features.Users;

//[DisplayName("List Users")]
//public class ListUsers(ISender sender) : ICommand<NoCommandArguments>
//{
//    public override async Task ExecuteAsync(NoCommandArguments arguments)
//    {
//        AnsiConsole.Write(new FigletText("List Users"));

//        var users = await sender.Send(new TodoApp.Application.Features.Users.GetUsers());
//        var table = new Table()
//            .Border(TableBorder.Ascii)
//            .AddColumn("FirstName")
//            .AddColumn("LastName")
//            .AddColumn("Subscription")
//            .AddColumn("Roles");

//        //table.Border = TableBorder.Rounded;

//        users.ForEach(x => table.AddRow(x.FirstName ?? "", x.LastName ?? "", x.SubscriptionLevel ?? "", x.Roles.ToString() ?? ""));
//        AnsiConsole.Write(table);
//    }
//}

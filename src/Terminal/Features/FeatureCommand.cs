//using Spectre.Console;
//using Terminal.CommandBus;

//namespace Terminal.Features;

//public class FeatureCommand : ICommand
//{
//    public ValueTask<ICommandArgument> ExecuteAsync(ICommandArgument arguments)
//    {
//        AnsiConsole.Write(new FigletText("Todo App"));

//        var selectedTable = AnsiConsole.Prompt(
//            new SelectionPrompt<string>()
//                .Title("Please make a selection")
//                .PageSize(10)
//                .AddChoices("Database", "Tasks", "Users"));
//    }
//}

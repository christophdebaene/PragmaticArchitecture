using System.ComponentModel;
using System.Reflection;

namespace Terminal.CommandBus;


public interface ICommand
{
    ValueTask<ICommandArgument> ExecuteAsync(ICommandArgument arguments);
    static string Name(Type type) => type.GetCustomAttribute<DisplayNameAttribute>(false)!.DisplayName!;
}

public interface ICommand<TArguments> where TArguments : ICommandArgument
{
    ValueTask<ICommandArgument> ExecuteAsync(TArguments arguments);

}

/*
public interface ICommand<in TArguments> : ICommand where TArguments : ICommandArgument
{
    Task ExecuteAsync(TArguments arguments);
}
public abstract class Command<TArguments> : ICommand<TArguments> where TArguments : ICommandArgument
{
    public abstract Task ExecuteAsync(TArguments arguments);
    async Task ICommand.ExecuteAsync(ICommandArgument arguments) => await ExecuteAsync((TArguments)arguments);
}

*/

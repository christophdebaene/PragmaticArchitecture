using System.ComponentModel;
using System.Reflection;

namespace Terminal.CommandBus;
public interface ICommand
{
    Task ExecuteAsync(ICommandArguments arguments);
    static string Name(Type type) => type.GetCustomAttribute<DisplayNameAttribute>(false)!.DisplayName!;
}
public interface ICommand<in TArguments> : ICommand where TArguments : ICommandArguments
{
    Task ExecuteAsync(TArguments arguments);
}
public abstract class Command<TArguments> : ICommand<TArguments> where TArguments : ICommandArguments
{
    public abstract Task ExecuteAsync(TArguments arguments);
    async Task ICommand.ExecuteAsync(ICommandArguments arguments) => await ExecuteAsync((TArguments)arguments);
}

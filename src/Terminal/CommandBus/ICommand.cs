using System.ComponentModel;
using System.Reflection;

namespace Terminal.CommandBus
{
    public interface ICommand
    {
        Task ExecuteAsync(CommandArguments arguments);
        static string Name(Type type) => type.GetCustomAttribute<DisplayNameAttribute>(false)!.DisplayName!;
    }
    public interface ICommand<in TArguments> : ICommand where TArguments : CommandArguments
    {
        Task ExecuteAsync(TArguments arguments);
    }
    public abstract class Command<TArguments> : ICommand<TArguments> where TArguments : CommandArguments
    {
        public abstract Task ExecuteAsync(TArguments arguments);
        async Task ICommand.ExecuteAsync(CommandArguments arguments) => await ExecuteAsync((TArguments)arguments);
    }
}

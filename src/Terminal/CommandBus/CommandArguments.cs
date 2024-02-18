namespace Terminal.CommandBus;
public interface ICommandArguments
{
}
public record NoCommandArguments : ICommandArguments
{
    public static NoCommandArguments Value = new();
}

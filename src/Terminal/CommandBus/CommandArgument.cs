namespace Terminal.CommandBus;
public interface ICommandArgument
{
}
public record NoCommandArguments : ICommandArgument
{
    public static NoCommandArguments Value = new();
}

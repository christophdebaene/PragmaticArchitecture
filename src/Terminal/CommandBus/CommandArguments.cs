namespace Terminal.CommandBus;
public abstract class CommandArguments
{
}
public class NoCommandArguments : CommandArguments
{
    public static CommandArguments Value = new NoCommandArguments();
}

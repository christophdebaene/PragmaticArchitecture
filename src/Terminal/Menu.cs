using Terminal.Misc;
using Terminal.Users;

namespace Terminal;
public static class Menu
{
    public static CompositeCommandArguments Main => new() { Commands = [typeof(ListUsers), typeof(AddUser), typeof(CreateDatabase)] };
}

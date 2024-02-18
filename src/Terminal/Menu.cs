using Terminal.Features.Database;
using Terminal.Features.Users;

namespace Terminal;
public static class Menu
{
    public static CompositeCommandArguments Main => new() { Commands = [typeof(ListUsers), typeof(ListTables), typeof(AddUser), typeof(CreateDatabase)] };
}

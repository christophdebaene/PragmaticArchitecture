
namespace Terminal;
/*
public static class MenuItem
{
    public static CompositeCommandArguments Main => new() { Commands = [typeof(ListUsers), typeof(ListTables), typeof(AddUser), typeof(CreateDatabase)] };
}
*/

public class MenuItem
{
    public Type CommandType { get; set; }
    public List<MenuItem> Items { get; set; } = [];
    public MenuItem Parent { get; set; }
    public MenuItem AddChild(Type command)
    {
        var item = new MenuItem
        {
            CommandType = command
        };

        Items.Add(item);
        item.Parent = this;
        return item;
    }
}

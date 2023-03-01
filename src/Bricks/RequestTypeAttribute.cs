using System.Reflection;

namespace Bricks;

[AttributeUsage(AttributeTargets.Class)]
public class RequestTypeAttribute : Attribute
{
    public RequestType RequestType { get; }
    public RequestTypeAttribute(RequestType type) => RequestType = type;
    public static RequestType Get(Type type)
    {
        var attr = type.GetCustomAttribute<RequestTypeAttribute>(false);
        return attr == null ? RequestType.Unknown : attr.RequestType;
    }
}
public class QueryAttribute : RequestTypeAttribute
{
    public QueryAttribute() : base(RequestType.Query)
    {
    }
}
public class CommandAttribute : RequestTypeAttribute
{
    public CommandAttribute() : base(RequestType.Command)
    {
    }
}

using Mediator;

namespace Bricks;
public static class TypeExtensions
{        
    public static bool IsCommand(this Type type)
    {
        if (type is null)
            return false;
        
        if (typeof(ICommand).IsAssignableFrom(type))
            return true;

        return type.ImplementsOpenGeneric(typeof(ICommand<>))
            || type.ImplementsOpenGeneric(typeof(IStreamCommand<>))
            || type.ImplementsOpenGeneric(typeof(IBaseCommand)); // in case you use your own derivations
    }    
    public static bool IsQuery(this Type type)
    {
        if (type is null)
            return false;
        
        return type.ImplementsOpenGeneric(typeof(IQuery<>))
            || type.ImplementsOpenGeneric(typeof(IStreamQuery<>))
            || typeof(IBaseQuery).IsAssignableFrom(type);
    }    
    private static bool ImplementsOpenGeneric(this Type type, Type openGeneric)
    {
        if (type is null || openGeneric is null)
            return false;
        
        if (!openGeneric.IsGenericTypeDefinition)
        {
            return openGeneric.IsAssignableFrom(type);
        }
        
        return type.GetInterfaces()
                    .Concat(type.IsInterface ? [type] : Array.Empty<Type>())
                    .Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == openGeneric);
    }
}

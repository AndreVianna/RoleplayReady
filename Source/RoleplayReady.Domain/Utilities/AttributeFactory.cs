namespace RoleplayReady.Domain.Utilities;

public static class AttributeFactory
{
    public static object CreateTypedAttribute(Type type, IEntity parent, string ownerId, string name, string? description = null)
        => Activator.CreateInstance(typeof(ElementAttribute<>).MakeGenericType(type), parent, ownerId, name, null, description)!;
}
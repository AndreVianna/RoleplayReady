using RolePlayReady.Models;
using RolePlayReady.Models.Contracts;

namespace RolePlayReady.Utilities;

public static class AttributeFactory
{
    public static object CreateTypedAttribute(Type type, IEntity parent, string ownerId, string name, string description)
        => Activator.CreateInstance(typeof(EntityAttribute).MakeGenericType(type), parent, ownerId, name, null, description)!;
}
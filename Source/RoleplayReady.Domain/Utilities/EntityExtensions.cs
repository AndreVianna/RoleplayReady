namespace RolePlayReady.Utilities;

internal static class EntityExtensions {
    public static object? GetValue(this IEntity element, string name)
        => element.GetAttribute(name).Value;

    public static TValue? GetValue<TValue>(this IEntity element, string name)
        => element.Attributes.OfType<ISimpleAttribute<TValue>>().First(p => p.AttributeDefinition.Name == name).Value;
    public static bool GetFlag<TValue>(this IEntity element, string name)
        => element.Attributes.OfType<IFlagAttribute>().First(p => p.AttributeDefinition.Name == name).Value;

    public static HashSet<TValue> GetList<TValue>(this IEntity element, string name)
        => element.Attributes.OfType<Models.Contracts.ISetAttribute<TValue>>().First(p => p.AttributeDefinition.Name == name).Value;

    public static Dictionary<TKey, TValue>? GetMap<TKey, TValue>(this IEntity element, string name)
        where TKey : notnull
        => element.Attributes.OfType<IMapAttribute<TKey, TValue>>().First(p => p.AttributeDefinition.Name == name).Value;

    public static IAttribute GetAttribute(this IEntity element, string name)
        => element.Attributes.First(p => p.AttributeDefinition.Name == name);

    public static bool Exists(this IEntity element, string name)
        => element.Attributes.Any(p => p.AttributeDefinition.Name == name);

    public static IAttribute? FindAttribute(this IEntity element, string name)
        => element.Attributes.FirstOrDefault(p => p.AttributeDefinition.Name == name);

    //public static EntityBuilder Configure(this IEntity element, string section) =>
    //    EntityBuilder.For(element, section);
}

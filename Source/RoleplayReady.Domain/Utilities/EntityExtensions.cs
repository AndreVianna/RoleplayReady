namespace RolePlayReady.Utilities;

internal static class EntityExtensions {
    public static object? GetValue(this IEntity element, string name)
        => element.GetAttribute(name).Value;

    public static TValue? GetValue<TValue>(this IEntity element, string name)
        => element.Attributes.OfType<IEntitySimpleAttribute<TValue>>().First(p => p.Attribute.Name == name).Value;
    public static bool GetFlag<TValue>(this IEntity element, string name)
        => element.Attributes.OfType<IEntityFlagAttribute>().First(p => p.Attribute.Name == name).Value;

    public static HashSet<TValue> GetList<TValue>(this IEntity element, string name)
        => element.Attributes.OfType<Models.Contracts.IEntitySetAttribute<TValue>>().First(p => p.Attribute.Name == name).Value;

    public static Dictionary<TKey, TValue>? GetMap<TKey, TValue>(this IEntity element, string name)
        where TKey : notnull
        => element.Attributes.OfType<IEntityDictionaryAttribute<TKey, TValue>>().First(p => p.Attribute.Name == name).Value;

    public static IEntityAttribute GetAttribute(this IEntity element, string name)
        => element.Attributes.First(p => p.Attribute.Name == name);

    public static bool Exists(this IEntity element, string name)
        => element.Attributes.Any(p => p.Attribute.Name == name);

    public static IEntityAttribute? FindAttribute(this IEntity element, string name)
        => element.Attributes.FirstOrDefault(p => p.Attribute.Name == name);

    //public static EntityBuilder Configure(this IEntity element, string section) =>
    //    EntityBuilder.For(element, section);
}

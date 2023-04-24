//namespace RolePlayReady.Utilities;

//internal static class EntityExtensions {
//    public static object? GetValue(this IPersisted element, string name)
//        => element.GetAttribute(name).VoidResult;

//    public static TValue? GetValue<TValue>(this IPersisted element, string name)
//        => element.Attributes.OfType<IComponentAttribute<TValue>>().First(p => p.Definition.Name == name).VoidResult;
//    public static bool GetFlag<TValue>(this IPersisted element, string name)
//        => element.Attributes.OfType<IEntityFlagAttribute>().First(p => p.Definition.Name == name).VoidResult;

//    public static HashSet<TValue> GetList<TValue>(this IPersisted element, string name)
//        => element.Attributes.OfType<Models.Contracts.IEntitySetAttribute<TValue>>().First(p => p.Definition.Name == name).VoidResult;

//    public static Dictionary<TKey, TValue>? GetMap<TKey, TValue>(this IPersisted element, string name)
//        where TKey : notnull
//        => element.Attributes.OfType<IEntityDictionaryAttribute<TKey, TValue>>().First(p => p.Definition.Name == name).VoidResult;

//    public static IComponentAttribute GetAttribute(this IPersisted element, string name)
//        => element.Attributes.First(p => p.Definition.Name == name);

//    public static bool Exists(this IPersisted element, string name)
//        => element.Attributes.Any(p => p.Definition.Name == name);

//    public static IComponentAttribute? FindAttribute(this IPersisted element, string name)
//        => element.Attributes.FirstOrDefault(p => p.Definition.Name == name);

//    //public static EntityBuilder Configure(this IPersisted element, string section) =>
//    //    EntityBuilder.For(element, section);
//}

namespace RolePlayReady.Models.Attributes;

public record EntityDictionaryAttribute<TKey, TValue>
    : EntityAttribute<Dictionary<TKey, TValue>>
    where TKey : notnull;
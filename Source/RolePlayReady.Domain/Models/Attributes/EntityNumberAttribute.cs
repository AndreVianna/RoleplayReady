namespace RolePlayReady.Models.Attributes;

public record EntityNumberAttribute<TValue>
    : EntityAttribute<TValue>
    where TValue : IComparable<TValue>;
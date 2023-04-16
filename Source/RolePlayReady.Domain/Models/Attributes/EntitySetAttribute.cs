namespace RolePlayReady.Models.Attributes;

public record EntitySetAttribute<TValue>
    : EntityAttribute<HashSet<TValue>>;
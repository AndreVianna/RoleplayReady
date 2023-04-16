namespace RolePlayReady.Models.Attributes;

public record EntityListAttribute<TValue>
    : EntityAttribute<List<TValue>>;
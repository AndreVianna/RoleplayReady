namespace RolePlayReady.Models.Attributes;

public record EntityComplexTypeAttribute<TValue>
    : EntityAttribute<TValue>
    where TValue : class, IValidatable;
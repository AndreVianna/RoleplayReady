namespace RolePlayReady.Models.Attributes;

public record EntityValidatableAttribute<TValue>
    : EntityAttribute<TValue>
    where TValue : class, IValidatable;
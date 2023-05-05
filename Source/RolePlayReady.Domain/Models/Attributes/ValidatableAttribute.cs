using System.Validation;

namespace RolePlayReady.Models.Attributes;

public record ValidatableAttribute<TValue>
    : Attribute<TValue>
    where TValue : class, IValidatable;
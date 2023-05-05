using System.Validation.Abstractions;

namespace RolePlayReady.Models.Abstractions;

public interface IAttribute : IValidatable {
    AttributeDefinition Definition { get; }
    object? Value { get; }
}

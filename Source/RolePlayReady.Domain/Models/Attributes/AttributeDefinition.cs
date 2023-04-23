namespace RolePlayReady.Models.Attributes;

public record AttributeDefinition : Base, IAttributeDefinition {
    public required Type DataType { get; init; }

    public ICollection<IAttributeConstraint> Constraints { get; } = new List<IAttributeConstraint>();

    public sealed override string ToString() => $"{base.ToString()}: {DataType.GetName()}";
}
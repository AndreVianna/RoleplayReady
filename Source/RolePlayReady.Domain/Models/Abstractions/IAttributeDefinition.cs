namespace RolePlayReady.Models.Abstractions;

public interface IAttributeDefinition : IBase {
    Type DataType { get; }
    ICollection<IAttributeConstraint> Constraints { get; }
}
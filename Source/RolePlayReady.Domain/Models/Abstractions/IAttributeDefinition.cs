namespace RolePlayReady.Models.Abstractions;

public interface IAttributeDefinition : IDescribed, IValidatable {
    Type DataType { get; }
    ICollection<IAttributeConstraint> Constraints { get; }
}
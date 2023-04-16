namespace RolePlayReady.Models.Abstractions;

public interface IAttributeDefinition : IDescribed {
    ICollection<IAttributeConstraint> Constraints { get; }
}
namespace RolePlayReady.Models.Contracts;

public interface ISetting : IEntity {
    new string ShortName { get; }
    IList<IAttributeDefinition> AttributeDefinitions { get; }
}
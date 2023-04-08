namespace RolePlayReady.Models.Contracts;

public interface IRuleSet : IEntity {
    new string ShortName { get; }
    IList<IAttributeDefinition> AttributeDefinitions { get; }
}
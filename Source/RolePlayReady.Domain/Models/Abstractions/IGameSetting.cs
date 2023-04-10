namespace RolePlayReady.Models.Abstractions;

public interface IGameSetting : IBase {
    IList<IAttributeDefinition> AttributeDefinitions { get; }
}
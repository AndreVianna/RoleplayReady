namespace RolePlayReady.Models.Abstractions;

public interface IGameSystemSetting : IBase<Guid> {
    IList<IAttributeDefinition> AttributeDefinitions { get; }
}
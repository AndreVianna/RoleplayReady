namespace RolePlayReady.Models.Contracts;

public interface IGameSetting : IBase {
    IList<IAttribute> AttributeDefinitions { get; }
}
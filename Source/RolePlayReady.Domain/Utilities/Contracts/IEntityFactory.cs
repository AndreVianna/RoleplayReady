namespace RolePlayReady.Utilities.Contracts;

public interface IEntityFactory {
    TComponent Create<TComponent>(string name, string description)
        where TComponent : IEntity;

    IEntity Create(string type, string name, string description);
}
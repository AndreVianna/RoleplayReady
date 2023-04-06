namespace RolePlayReady.Utilities.Contracts;

public interface IComponentFactory {
    TComponent Create<TComponent>(string abbreviation, string name, string description)
        where TComponent : IComponent;

    IComponent Create(string type, string abbreviation, string name, string description);
}
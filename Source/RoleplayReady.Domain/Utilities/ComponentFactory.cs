using RolePlayReady.Models;
using RolePlayReady.Utilities.Contracts;

using Object = RolePlayReady.Models.Object;

namespace RolePlayReady.Utilities;

public class ComponentFactory : IComponentFactory {
    private readonly IComponent _parent;
    private readonly string _ownerId;

    private ComponentFactory(IComponent parent, string ownerId) {
        _parent = parent;
        _ownerId = ownerId;
    }

    public static IComponentFactory For(IComponent parent, string ownerId) => new ComponentFactory(parent, ownerId);

    public TComponent Create<TComponent>(string abbreviation, string name, string description)
        where TComponent : IComponent
        => (TComponent)Create(typeof(TComponent).Name, abbreviation, name, description);

    public IComponent Create(string type, string abbreviation, string name, string description)
        => type switch {
            nameof(Agent) => new Agent(_parent, abbreviation, name, description),
            //nameof(Component) => new Component(_parent, abbreviation, name, description),
            nameof(Power) => new Power(_parent, abbreviation, name, description),
            nameof(PowerSource) => new PowerSource(_parent, abbreviation, name, description),
            nameof(Object) => new Object(_parent, abbreviation, name, description, string.Empty),
            _ => throw new ArgumentException($"Unknown type: {type}", nameof(type))
        };
}
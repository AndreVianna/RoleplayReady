using Object = RoleplayReady.Domain.Models.Object;

namespace RoleplayReady.Domain.RuleSets;

public class ElementFactory : IElementFactory {
    private readonly IEntity _parent;
    private readonly string _ownerId;

    private ElementFactory(IEntity parent, string ownerId) {
        _parent = parent;
        _ownerId = ownerId;
    }

    public static IElementFactory For(IEntity parent, string ownerId) => new ElementFactory(parent, ownerId);

    public IElement Create(string type, string name, string? description = null)
        => type switch {
            nameof(Actor) => new Actor(_parent, _ownerId, name, description),
            nameof(Component) => new Component(_parent, _ownerId, name, description),
            nameof(Power) => new Power(_parent, _ownerId, name, description),
            nameof(PowerSource) => new PowerSource(_parent, _ownerId, name, description),
            nameof(Object) => new Object(_parent, _ownerId, name, description),
            _ => throw new ArgumentException($"Unknown type: {type}", nameof(type))
        };
}
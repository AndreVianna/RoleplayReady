namespace RoleplayReady.Domain.Models;

public record Component : Element, IComponent {
    public Component() {
        
    }

    [SetsRequiredMembers]
    public Component(IEntity parent, string ownerId, string name, string? description = null)
        : base(parent, ownerId, name, description) { }
}
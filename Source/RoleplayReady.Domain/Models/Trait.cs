namespace RoleplayReady.Domain.Models;

public record Trait : Element, ITrait {
    public Trait() {
        
    }

    [SetsRequiredMembers]
    public Trait(IEntity parent, string ownerId, string name, string? description = null)
        : base(parent, ownerId, name, description) { }
}
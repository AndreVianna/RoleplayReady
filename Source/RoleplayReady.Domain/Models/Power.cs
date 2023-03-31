namespace RoleplayReady.Domain.Models;

public record Power : Element, IPower {
    public Power() {
        
    }

    [SetsRequiredMembers]
    public Power(IEntity parent, string ownerId, string name, string? description = null)
        : base(parent, ownerId, name, description) { }
}
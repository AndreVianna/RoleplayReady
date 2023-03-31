namespace RoleplayReady.Domain.Models;

public record Trait : Element, ITrait {
    public Trait() {

    }

    [SetsRequiredMembers]
    public Trait(IEntity parent, string ownerId, string name, string? description = null, Status? status = null, Usage? usage = null, ISource? source = null)
        : base(parent, ownerId, name, description, status, usage, source) { }
}
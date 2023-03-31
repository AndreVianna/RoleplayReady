namespace RoleplayReady.Domain.Models;

public record Trigger : Element, ITrigger {
    public Trigger() {

    }

    [SetsRequiredMembers]
    public Trigger(IEntity parent, string ownerId, string name, string? description = null, Status? status = null, Usage? usage = null, ISource? source = null)
        : base(parent, ownerId, name, description, status, usage, source) { }
}
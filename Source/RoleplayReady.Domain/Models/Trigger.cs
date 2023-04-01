namespace RoleplayReady.Domain.Models;

public record Trigger : Element, ITrigger {
    public Trigger() { }

    [SetsRequiredMembers]
    public Trigger(IEntity parent, string ownerId, string abbreviation, string name, string description, State? state = null, Usage? usage = null, ISource? source = null)
        : base(parent, ownerId, abbreviation, name, description, state, usage, source) { }

    [SetsRequiredMembers]
    public Trigger(IEntity parent, string ownerId, string name, string description, State? state = null, Usage? usage = null, ISource? source = null) :
        base(parent, ownerId, name, description, state, usage, source) { }
}
namespace RoleplayReady.Domain.Models;

public record Power : Element, IPower {
    public Power() { }

    [SetsRequiredMembers]
    public Power(IEntity parent, string ownerId, string abbreviation, string name, string description, State? state = null, Usage? usage = null, ISource? source = null)
        : base(parent, ownerId, abbreviation, name, description, state, usage, source) {
    }

    [SetsRequiredMembers]
    public Power(IEntity parent, string ownerId, string name, string description, State? state = null, Usage? usage = null, ISource? source = null) :
        base(parent, ownerId, name, description, state, usage, source) { }
}
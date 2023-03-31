namespace RoleplayReady.Domain.Models;

public record Power : Element, IPower {
    public Power() {

    }

    [SetsRequiredMembers]
    public Power(IEntity parent, string ownerId, string name, string? description = null, Status? status = null, Usage? usage = null, ISource? source = null)
        : base(parent, ownerId, name, description, status, usage, source) { }
}
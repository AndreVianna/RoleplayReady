namespace RoleplayReady.Domain.Models;

public record PowerSource : Element, IPowerSource {
    public PowerSource() {

    }

    [SetsRequiredMembers]
    public PowerSource(IEntity parent, string ownerId, string name, string? description = null, Status? status = null, Usage? usage = null, ISource? source = null)
        : base(parent, ownerId, name, description, status, usage, source) { }
}
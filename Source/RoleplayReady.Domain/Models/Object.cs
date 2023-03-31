namespace RoleplayReady.Domain.Models;

public record Object : Element, IObject {
    public Object() {

    }

    [SetsRequiredMembers]
    public Object(IEntity parent, string ownerId, string name, string? description = null, Status? status = null, Usage? usage = null, ISource? source = null)
        : base(parent, ownerId, name, description, status, usage, source) { }
}
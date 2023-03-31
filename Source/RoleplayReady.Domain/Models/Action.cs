namespace RoleplayReady.Domain.Models;

public record Action : Element, IAction {
    public Action() {

    }

    [SetsRequiredMembers]
    public Action(IEntity parent, string ownerId, string name, string? description = null, Status? status = null, Usage? usage = null, ISource? source = null)
        : base(parent, ownerId, name, description, status, usage, source) { }
}
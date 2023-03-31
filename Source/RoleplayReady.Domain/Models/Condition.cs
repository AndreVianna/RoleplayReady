namespace RoleplayReady.Domain.Models;

public record Condition : Element, ICondition {
    public Condition() {

    }

    [SetsRequiredMembers]
    public Condition(IEntity parent, string ownerId, string name, string? description = null, Status? status = null, Usage? usage = null, ISource? source = null)
        : base(parent, ownerId, name, description, status, usage, source) { }
}

namespace RolePlayReady.Models;

public record Trigger : Entity, ITrigger {
    public Trigger() { }

    [SetsRequiredMembers]
    public Trigger(INode parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime)
        : base(parent, abbreviation, name, description, dateTime) { }
}
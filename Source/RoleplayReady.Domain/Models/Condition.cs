namespace RolePlayReady.Models;

public record Condition : Entity, ICondition {
    public Condition() { }

    [SetsRequiredMembers]
    public Condition(INode parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime)
        : base(parent, abbreviation, name, description, dateTime) { }
}

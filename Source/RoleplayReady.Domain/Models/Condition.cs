namespace RoleplayReady.Domain.Models;

public record Condition : Component, ICondition {
    public Condition() { }

    [SetsRequiredMembers]
    public Condition(IComponent? parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime)
        : base(parent, abbreviation, name, description, dateTime) { }
}

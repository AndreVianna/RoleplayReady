namespace RoleplayReady.Domain.Models;

public record Trigger : Component, ITrigger {
    public Trigger() { }

    [SetsRequiredMembers]
    public Trigger(IComponent? parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime)
        : base(parent, abbreviation, name, description, dateTime) { }
}
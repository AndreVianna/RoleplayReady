namespace RoleplayReady.Domain.Models;

public record Trait : Component, ITrait {
    public Trait() { }

    [SetsRequiredMembers]
    public Trait(IComponent? parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime)
        : base(parent, abbreviation, name, description, dateTime) { }
}
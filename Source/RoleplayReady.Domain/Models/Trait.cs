namespace RolePlayReady.Models;

public record Trait : Entity, ITrait {
    public Trait() { }

    [SetsRequiredMembers]
    public Trait(INode parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime)
        : base(parent, abbreviation, name, description, dateTime) { }
}
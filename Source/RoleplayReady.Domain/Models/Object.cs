namespace RolePlayReady.Models;

public record Object : Entity, IObject {
    public Object() { }

    [SetsRequiredMembers]
    public Object(INode parent, string abbreviation, string name, string description, string unit, IDateTimeProvider? dateTimeProvider = null)
        : base(parent, abbreviation, name, description, dateTimeProvider) {
        Unit = Throw.IfNullOrWhiteSpaces(unit);
    }

    public required string Unit { get; init; }
}
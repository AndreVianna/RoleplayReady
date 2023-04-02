namespace RoleplayReady.Domain.Models;

public record Object : Component, IObject {
    public Object() { }

    [SetsRequiredMembers]
    public Object(IComponent parent, string abbreviation, string name, string description, string unit, IDateTimeProvider? dateTimeProvider = null)
        : base(parent, abbreviation, name, description, dateTimeProvider) {
        Unit = Throw.IfNullOrWhiteSpaces(unit);
    }

    public required string Unit { get; init; }
}
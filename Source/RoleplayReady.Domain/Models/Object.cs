using RolePlayReady.Utilities;
using RolePlayReady.Utilities.Contracts;

namespace RolePlayReady.Models;

public record Object : Component, IObject {
    public Object() { }

    [SetsRequiredMembers]
    public Object(IComponent parent, string abbreviation, string name, string description, string unit, IDateTimeProvider? dateTimeProvider = null)
        : base(parent, abbreviation, name, description, dateTimeProvider) {
        Unit = Throw.IfNullOrWhiteSpaces(unit);
    }

    public required string Unit { get; init; }
}
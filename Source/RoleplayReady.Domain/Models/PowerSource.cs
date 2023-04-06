using RolePlayReady.Utilities.Contracts;

namespace RolePlayReady.Models;

public record PowerSource : Component, IPowerSource {
    public PowerSource() { }

    [SetsRequiredMembers]
    public PowerSource(IComponent? parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime = null)
        : base(parent, abbreviation, name, description, dateTime) { }
}
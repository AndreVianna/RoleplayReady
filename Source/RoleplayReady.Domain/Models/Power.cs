namespace RoleplayReady.Domain.Models;

public record Power : Component, IPower {
    public Power() { }

    [SetsRequiredMembers]
    public Power(IComponent? parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime = null)
        : base(parent, abbreviation, name, description, dateTime) { }
}
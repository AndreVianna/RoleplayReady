namespace RolePlayReady.Models;

public record Power : Entity, IPower {
    public Power() { }

    [SetsRequiredMembers]
    public Power(INode parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime = null)
        : base(parent, abbreviation, name, description, dateTime) { }
}
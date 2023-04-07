namespace RolePlayReady.Models;

public record PowerSource : Entity, IPowerSource {
    public PowerSource() { }

    [SetsRequiredMembers]
    public PowerSource(INode parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime = null)
        : base(parent, abbreviation, name, description, dateTime) { }
}
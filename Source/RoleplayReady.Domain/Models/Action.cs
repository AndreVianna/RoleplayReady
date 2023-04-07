namespace RolePlayReady.Models;

public record Action : Entity, IAction {
    public Action() { }

    [SetsRequiredMembers]
    public Action(INode parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime)
        : base(parent, abbreviation, name, description, dateTime) { }
}
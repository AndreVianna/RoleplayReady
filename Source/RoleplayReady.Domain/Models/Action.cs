namespace RolePlayReady.Models;

public record Action : Component, IAction {
    public Action() { }

    [SetsRequiredMembers]
    public Action(IComponent? parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime)
        : base(parent, abbreviation, name, description, dateTime) { }
}
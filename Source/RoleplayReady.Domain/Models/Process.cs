namespace RoleplayReady.Domain.Models;

public record Process : Component, IProcess {
    public Process() { }

    [SetsRequiredMembers]
    public Process(IComponent parent, string abbreviation, string name, string description, IProcessStep start, IDateTimeProvider? dateTimeProvider)
        : base(parent, abbreviation, name, description, dateTimeProvider) {
        Start = start;
    }

    public required IProcessStep Start { get; init; }
}
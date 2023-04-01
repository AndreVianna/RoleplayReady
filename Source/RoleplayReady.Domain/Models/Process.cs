namespace RoleplayReady.Domain.Models;

public record Process : Element, IProcess {
    public Process() { }

    [SetsRequiredMembers]
    public Process(IEntity parent, string ownerId, string abbreviation, string name, string description, IProcessStep start, State? state = null, Usage? usage = null,
        ISource? source = null)
        : base(parent, ownerId, abbreviation, name, description, state, usage, source) {
        Start = start;
    }

    [SetsRequiredMembers]
    public Process(IEntity parent, string ownerId, string name, string description, IProcessStep start, State? state = null, Usage? usage = null, ISource? source = null) :
        this(parent, ownerId, name.ToAcronym(), name, description, start, state, usage, source) { }

    public required IProcessStep Start { get; init; }
}
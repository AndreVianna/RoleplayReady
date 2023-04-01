namespace RoleplayReady.Domain.Models;

public record Object : Element, IObject {
    public Object() { }

    [SetsRequiredMembers]
    public Object(IEntity parent, string ownerId, string abbreviation, string name, string description, string unit, State? state = null, Usage? usage = null,
        ISource? source = null)
        : base(parent, ownerId, abbreviation, name, description, state, usage, source) {
        Unit = unit ?? throw new ArgumentNullException(nameof(unit));
    }

    [SetsRequiredMembers]
    public Object(IEntity parent, string ownerId, string name, string description, string unit, State? state = null, Usage? usage = null, ISource? source = null) :
        this(parent, ownerId, name.ToAcronym(), name, description, unit, state, usage, source) { }

    public required string Unit { get; init;  }
}
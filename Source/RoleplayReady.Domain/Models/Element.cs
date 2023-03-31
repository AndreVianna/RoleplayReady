namespace RoleplayReady.Domain.Models;

public record Element : Child, IElement {
    public Element() {
        
    }

    [SetsRequiredMembers]
    public Element(IEntity parent, string ownerId, string name, string? description = null)
        : base(parent, ownerId, name, description) {
    }

    public Usage Usage { get; init; } = Usage.Standard;

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public Status Status { get; init; } = Status.NotReady;

    public ISource? Source { get; init; }

    public IList<string> Tags { get; init; } = new List<string>();
    public IList<IValidation> Requirements { get; init; } = new List<IValidation>();
    public IList<IElementAttribute> Attributes { get; init; } = new List<IElementAttribute>();
    public IList<ITrait> Traits { get; init; } = new List<ITrait>();
    public IList<IPowerSource> PowerSources { get; init; } = new List<IPowerSource>();
    public IList<IEffects> Effects { get; init; } = new List<IEffects>();
    public IList<ITrigger> Triggers { get; init; } = new List<ITrigger>();
    public IList<IValidation> Validations { get; init; } = new List<IValidation>();
}
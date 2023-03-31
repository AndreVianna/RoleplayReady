namespace RoleplayReady.Domain.Models;

public record Element : Child, IElement {
    public Element() {

    }

    [SetsRequiredMembers]
    public Element(IEntity parent, string ownerId, string name, string? description = null, Status? status = null, Usage? usage = null, ISource? source = null)
        : base(parent, ownerId, name, description, status) {
        if (parent is IElement element) {
            Usage = usage ?? element.Usage;
            Status = status ?? element.Status;
            Source = source ?? element.Source;
        }
        else {
            Usage = usage ?? Usage.Standard;
            Status = status ?? Status.NotReady;
            Source = source;
        }
    }

    public required Usage Usage { get; init; }
    public required ISource? Source { get; init; }

    public IList<string> Tags { get; init; } = new List<string>();
    public IList<IValidation> Requirements { get; init; } = new List<IValidation>();
    public IList<IElementAttribute> Attributes { get; init; } = new List<IElementAttribute>();
    public IList<ITrait> Traits { get; init; } = new List<ITrait>();
    public IList<IPowerSource> PowerSources { get; init; } = new List<IPowerSource>();
    public IList<IEffects> Effects { get; init; } = new List<IEffects>();
    public IList<ITrigger> Triggers { get; init; } = new List<ITrigger>();
    public IList<IValidation> Validations { get; init; } = new List<IValidation>();
}
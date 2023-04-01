namespace RoleplayReady.Domain.Models;

public abstract record Element : Entity, IElement {
    public Element() {
    }

    [SetsRequiredMembers]
    protected Element(IEntity parent, string ownerId, string abbreviation, string name, string description, State? state = null, Usage? usage = null, ISource? source = null)
        : base(parent, ownerId, abbreviation, name, description, state) {
            Usage = usage ?? (parent as IElement)?.Usage ?? Usage.Standard;
            Source = source ?? (parent as IElement)?.Source;
    }

    [SetsRequiredMembers]
    protected Element(IEntity parent, string ownerId, string name, string description, State? state = null, Usage? usage = null, ISource? source = null) :
        this(parent, ownerId, name.ToAcronym(), name, description, state, usage, source) {
    }

    public Usage Usage { get; init; } = Usage.Standard;
    public ISource? Source { get; init; }

    public IList<string> Tags { get; init; } = new List<string>();

    public IList<IValidation> Requirements { get; init; } = new List<IValidation>();
    public IList<IElementAttribute> Attributes { get; init; } = new List<IElementAttribute>();
    public IList<IValidation> Validations { get; init; } = new List<IValidation>();

    public IList<IEffects> Effects { get; init; } = new List<IEffects>();

    public IList<IElement> Elements { get; init; } = new List<IElement>();

    public IList<ITrait> Traits { get; init; } = new List<ITrait>();
    public IList<IPowerSource> PowerSources { get; init; } = new List<IPowerSource>();
    public IList<ITrigger> Triggers { get; init; } = new List<ITrigger>();
}
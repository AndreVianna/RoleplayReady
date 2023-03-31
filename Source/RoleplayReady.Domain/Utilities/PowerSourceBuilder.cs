namespace RoleplayReady.Domain.Utilities;

internal class PowerSourceBuilder : IBuilder {
    private readonly IElement _parent;
    private readonly string _ownerId;
    private readonly Usage _usage;
    private readonly Status _status;
    private readonly ISource? _source;

    private PowerSourceBuilder(IElement parent, string ownerId, Usage usage = Usage.Closed, Status status = Status.NotReady, ISource? source = null) {
        _parent = parent;
        _ownerId = ownerId;
        _usage = usage;
        _status = status;
        _source = source;
    }

    public static IBuilder For(IElement parent, string ownerId, Usage usage = Usage.Closed, Status status = Status.Public, ISource? source = null)
        => new PowerSourceBuilder(parent, ownerId, usage, status, source);

    public IBuilder Add(string name, Action<IFluentBuilder> build)
        => Add(name, null!, build);

    public IBuilder Add(string name, string description, Action<IFluentBuilder> build) {
        var trait = new PowerSource(_parent, _ownerId, name, description) { Usage = _usage, Status = _status, Source = _source };
        build(AttributeEffectsBuilder.For(trait));
        return Add(trait);
    }

    private IBuilder Add(IPowerSource powerSource) {
        _parent.PowerSources.Add(powerSource);
        return this;
    }
}
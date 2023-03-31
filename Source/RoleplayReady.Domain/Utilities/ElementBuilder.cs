namespace RoleplayReady.Domain.Utilities;

public class ElementBuilder {
    private readonly IElement _element;
    private readonly string _section;
    private string? _ownerId;
    private Usage? _usage;
    private Status? _status;
    private ISource? _source;

    private ElementBuilder(IElement element, string section) {
        _element = element;
        _section = section;
    }

    public static ElementBuilder For(IElement element, string section) => new ElementBuilder(element, section);

    public ElementBuilder For(string ownerId) {
        _ownerId = ownerId;
        return this;
    }

    public ElementBuilder WithUsage(Usage usage) {
        _usage = usage;
        return this;
    }

    public ElementBuilder WithStatus(Status status) {
        _status = status;
        return this;
    }

    public ElementBuilder WithSource(ISource? source) {
        _source = source;
        return this;
    }

    public void As(Action<IBuilder> configure) {
        switch (_section) {
            case nameof(Element.Traits) :
                configure(TraitsBuilder.For(_element, _ownerId ?? _element.OwnerId, _usage ?? _element.Usage, _status ?? _element.Status, _source ?? _element.Source));
                return;
            case nameof(Element.PowerSources):
                configure(PowerSourceBuilder.For(_element, _ownerId ?? _element.OwnerId, _usage ?? _element.Usage, _status ?? _element.Status, _source ?? _element.Source));
                return;
            default: throw new NotImplementedException();
        };
    }
}
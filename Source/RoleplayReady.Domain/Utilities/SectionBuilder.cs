namespace RoleplayReady.Domain.Utilities;

public class SectionBuilder : ISectionBuilder {
    private readonly IElement _parent;
    private readonly string _section;
    private readonly string _sectionItem;

    private SectionBuilder(IElement parent, string section) {
        _parent = parent;
        _section = section;
        _sectionItem = _section switch {
            nameof(Element.Traits) => nameof(Trait),
            nameof(Element.PowerSources) => nameof(PowerSource),
            _ => throw new NotImplementedException()
        };
    }

    public static ISectionBuilder For(IElement element, string section) => new SectionBuilder(element, section);

    public ISectionBuilder Add(string name, Action<IFluentBuilder> configure)
        => Add(name, null!, configure);

    public ISectionBuilder Add(string name, string description, Action<IFluentBuilder> configure)
        => Add(name, description, (_, b) => configure(b));

    public ISectionBuilder Add(string name, Action<IElement, IFluentBuilder> configure)
        => Add(name, null!, configure);

    public ISectionBuilder Add(string name, string description, Action<IElement, IFluentBuilder> configure) {
        var factory = ElementFactory.For(_parent, _parent.OwnerId);
        var item = factory.Create(_sectionItem, name, description, _parent.Status, _parent.Usage, _parent.Source);
        var builder = SectionItemBuilder.For(item);
        configure(_parent, builder);
        switch (_section) {
            case nameof(Element.Traits) when item is Trait trait:
                _parent.Traits.Add(trait);
                return this;
            case nameof(Element.PowerSources) when item is PowerSource powerSource:
                _parent.PowerSources.Add(powerSource);
                return this;
            default:
                throw new NotImplementedException();
        };
    }
}
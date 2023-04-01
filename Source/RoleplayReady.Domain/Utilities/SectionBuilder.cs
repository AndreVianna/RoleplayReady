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

    public ISectionBuilder Add(string name, string description, Action<IFluentBuilder> configure)
        => Add(name, description, (_, x) => configure(x));

    public ISectionBuilder Add(string name, string description, Action<IElement, IFluentBuilder> configure) {
        var factory = ElementFactory.For(_parent, _parent.OwnerId);
        var item = factory.Create(_sectionItem, name, description, _parent.State, _parent.Usage, _parent.Source);
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

    public ISectionBuilder Remove(string existing) {
        var item = Find(existing);
        if (item is null) {
            throw new InvalidOperationException($"No {_sectionItem} named {existing} found.");
        }
        switch (_section) {
            case nameof(Element.Traits):
                _parent.Traits.Remove((ITrait)item);
                return this;
            case nameof(Element.PowerSources):
                _parent.PowerSources.Remove((IPowerSource)item);
                return this;
            default:
                throw new NotImplementedException();
        }
    }

    private IElement? Find(string existing) =>
        _section switch {
            nameof(Element.Traits) => _parent.Traits.FirstOrDefault(i => i.Name == existing),
            nameof(Element.PowerSources) => _parent.PowerSources.FirstOrDefault(i => i.Name == existing),
            _ => throw new NotImplementedException()
        };

    public ISectionBuilder Replace(string existing, string name, string description, Action<IFluentBuilder> configure)
        => Replace(existing, name, description, (_, x) => configure(x));

    public ISectionBuilder Replace(string existing, string name, string description, Action<IElement, IFluentBuilder> configure)
        => Remove(existing).Add(name, description, configure);

    public ISectionBuilder IncludeIn(string existing, string description, Action<IFluentBuilder> configure) {
        throw new NotImplementedException();
    }

    public ISectionBuilder IncludeIn(string existing, string description, Action<IElement, IFluentBuilder> configure) {
        throw new NotImplementedException();
    }
}
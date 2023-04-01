namespace RoleplayReady.Domain.Utilities;

public class SectionBuilder : ISectionBuilderReplaceContinuation, ISectionBuilderMainCommands, ISectionBuilderCommandConnector {
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

    public static ISectionBuilderMainCommands For(IElement element, string section) => new SectionBuilder(element, section);

    public ISectionBuilderMainCommands And => this;

    public ISectionBuilderCommandConnector Add(string name, string description, Action<IElementUpdaterMain> configure)
        => Add(name, description, (_, x) => configure(x));

    public ISectionBuilderCommandConnector Add(string name, string description, Action<IElement, IElementUpdaterMain> configure) {
        var factory = ElementFactory.For(_parent, _parent.OwnerId);
        var item = factory.Create(_sectionItem, name, description, _parent.State, _parent.Usage, _parent.Source);
        var builder = ElementUpdater.For(item);
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

    public ISectionBuilderCommandConnector Remove(string existing) {
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

    public ISectionBuilderReplaceContinuation Replace(string existing)
        => (ISectionBuilderReplaceContinuation)Remove(existing);

    public ISectionBuilderCommandConnector With(string name, string description, Action<IElementUpdaterMain> configure)
        => Add(name, description, (_, x) => configure(x));

    public ISectionBuilderCommandConnector With(string name, string description, Action<IElement, IElementUpdaterMain> configure)
        => Add(name, description, configure);

    public ISectionBuilderCommandConnector With(string description, Action<IElementUpdaterMain> configure)
        => Add(name, description, (_, x) => configure(x));

    public ISectionBuilderCommandConnector With(string description, Action<IElement, IElementUpdaterMain> configure)
        => Add(name, description, configure);

    public ISectionBuilderCommandConnector With(Action<IElementUpdaterMain> configure)
        => Add(name, description, (_, x) => configure(x));

    public ISectionBuilderCommandConnector With(Action<IElement, IElementUpdaterMain> configure)
        => Add(name, description, configure);

}
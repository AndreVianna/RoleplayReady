namespace RoleplayReady.Domain.Utilities;

public class SectionBuilder : ISectionBuilder {

    private SectionBuilder(IEntity entity, string section) {
        Target = entity;
        Section = section;
        SectionItem = Section switch {
            nameof(Element.Traits) => nameof(Trait),
            nameof(RuleSet.PowerSources) => nameof(PowerSource),
            _ => throw new NotImplementedException()
        };
    }

    public static ISectionBuilder.IMainCommands For(IEntity entity, string section) => new MainCommands(entity, section);

    protected IEntity Target { get; }
    protected string Section { get; }
    protected string SectionItem { get; }

    protected IElement? Find(string existing)
        => Section switch {
            nameof(Element.Traits) when Target is Element element => element.Traits.FirstOrDefault(i => i.Name == existing),
            nameof(RuleSet.PowerSources) when Target is RuleSet ruleSet => ruleSet.PowerSources.FirstOrDefault(i => i.Name == existing),
            _ => throw new NotImplementedException()
        };

    protected void Add(IElement item) {
        switch (Target) {
            case Element element when item is Trait trait:
                element.Traits.Add(trait);
                return;
            case RuleSet ruleSet when item is PowerSource powerSource:
                ruleSet.PowerSources.Add(powerSource);
                return;
            default:
                throw new NotImplementedException();
        }
    }

    protected void Remove(IElement item) {
        switch (Section) {
            case nameof(Element.Traits) when Target is Element element:
                element.Traits.Remove((ITrait)item);
                return;
            case nameof(RuleSet.PowerSources) when Target is RuleSet ruleSet:
                ruleSet.PowerSources.Remove((IPowerSource)item);
                return;
            default:
                throw new NotImplementedException();
        }
    }

    private class MainCommands : SectionBuilder, ISectionBuilder.IMainCommands {
        public MainCommands(IEntity entity, string section) : base(entity, section) {
        }

        public ISectionBuilder.IConnector Add(string name, string description, Action<IElementUpdater.IMain> configure)
            => Add(name, description, (_, x) => configure(x));

        public ISectionBuilder.IConnector Add(string name, string description, Action<IEntity, IElementUpdater.IMain> configure) {
            var factory = ElementFactory.For(Target, Target.OwnerId);
            var item = factory.Create(SectionItem, name, description);
            configure(Target, ElementUpdater.For(item));
            Add(item);
            return new Connector(Target, Section);
        }

        public ISectionBuilder.IConnector Remove(string existing) {
            var item = Find(existing) ?? throw new InvalidOperationException($"{SectionItem} named {existing} not found.");
            Remove(item);
            return new Connector(Target, Section);
        }

        public ISectionBuilder.IReplaceWith Replace(string existing) {
            Remove(existing);
            return new ReplaceWith(Target, Section);
        }

        public ISectionBuilder.IAppendWith Append(string existing) {
            var item = Find(existing) ?? throw new InvalidOperationException($"{SectionItem} named {existing} not found.");
            return new AppendWith(Target, Section, item);
        }
    }

    private class Connector : SectionBuilder, ISectionBuilder.IConnector {
        public Connector(IEntity entity, string section) : base(entity, section) {
        }

        public ISectionBuilder.IMainCommands And() => new MainCommands(Target, Section);
    }

    private class ReplaceWith : SectionBuilder, ISectionBuilder.IReplaceWith {
        public ReplaceWith(IEntity entity, string section) : base(entity, section) {
        }

        public ISectionBuilder.IConnector With(string name, string description, Action<IElementUpdater.IMain> configure) {
            var mainCommands = new MainCommands(Target, Section);
            return mainCommands.Add(name, description, (_, x) => configure(x));
        }

        public ISectionBuilder.IConnector With(string name, string description, Action<IEntity, IElementUpdater.IMain> configure) {
            var mainCommands = new MainCommands(Target, Section);
            return mainCommands.Add(name, description, configure);
        }
    }

    private class AppendWith : SectionBuilder, ISectionBuilder.IAppendWith {
        private IElement _original;

        public AppendWith(IEntity entity, string section, IElement original) : base(entity, section) {
            _original = original;
        }

        public ISectionBuilder.IConnector With(string additionalDescription, Action<IElementUpdater.IMain> configure)
            => With(additionalDescription, (_, e) => configure(e));

        public ISectionBuilder.IConnector With(string additionalDescription, Action<IEntity, IElementUpdater.IMain> configure) {
            _original = (Element)_original with {
                Description = $"{_original.Description}\n{additionalDescription}",
            };
            return With(configure);
        }

        public ISectionBuilder.IConnector With(Action<IElementUpdater.IMain> configure)
                => With((_, x) => configure(x));

        public ISectionBuilder.IConnector With(Action<IEntity, IElementUpdater.IMain> configure) {
            configure(Target, ElementUpdater.For(_original));
            Add(_original);
            return new Connector(Target, Section);
        }
    }
}
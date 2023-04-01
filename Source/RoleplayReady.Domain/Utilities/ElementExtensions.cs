using RoleplayReady.Domain.Models.Validations;

using Attribute = RoleplayReady.Domain.Models.Attribute;

namespace RoleplayReady.Domain.Utilities;

internal static class ElementExtensions {
    public static IElementAttribute<T?> GetAttribute<T>(this IElement element, string name)
        => element.Attributes.OfType<ElementAttribute<T?>>().First(p => p.Attribute.Name == name);

    public static IElementAttribute<T?>? FindAttribute<T>(this IElement element, string name)
        => element.Attributes.OfType<ElementAttribute<T?>>().FirstOrDefault(p => p.Attribute.Name == name);

    public static IPowerSource GetPowerSource(this IElement element, string name)
        => element.PowerSources.First(p => p.Name == name);

    public static IPowerSource? FindPowerSource(this IElement element, string name)
        => element.PowerSources.FirstOrDefault(p => p.Name == name);

    public static ElementBuilder Configure(this IElement element, string section) =>
        ElementBuilder.For(element, section);

    public static IElement CopyTraitsFrom(this IElement target, IElement source, params string[] excluding) {
        target.Traits.Clear();
        source.Traits.Where(i => excluding.Contains(i.Name)).ToList().ForEach(x => target.Traits.Add(x.Clone(target)));
        return target;
    }

    public static IElementAttribute Clone(this IElementAttribute source, IElement? parent = null) =>
        source switch {
            ElementAttribute elementAttribute => elementAttribute with {
                Element = parent ?? elementAttribute.Element,
                Attribute = elementAttribute.Attribute.Clone(parent),
                Value = elementAttribute.Value,
            },
            _ => source,
        };

    public static IPossession Clone(this IPossession source) => (Possession)source with { Object = source.Object.Clone() };

    public static TEntity Clone<TEntity>(this TEntity source, IElement? parent = null)
        where TEntity : class, IEntity {
        var target = source switch {
            Attribute attribute => attribute with {
                Parent = parent ?? attribute.Parent,
                Timestamp = DateTime.UtcNow,
                State = State.New,
                DataType = attribute.DataType,
            } as TEntity,
            Actor element => element with {
                Parent = parent ?? element.Parent,
                Timestamp = DateTime.UtcNow,
                State = State.New,
                Tags = new List<string>(),
                Requirements = new List<IValidation>(element.Requirements.ToArray()),
                Attributes = new List<IElementAttribute>(),
                Traits = new List<ITrait>(),
                PowerSources = new List<IPowerSource>(),
                Effects = new List<IEffects>(element.Effects.ToArray()),
                Triggers = new List<ITrigger>(),
                Validations = new List<IValidation>(element.Validations.ToArray()),
                Possessions = new List<IPossession>(),
                Powers = new List<IPower>(),
                Actions = new List<IAction>(),
                Conditions = new List<ICondition>(),
                JournalEntries = new List<IJournalEntry>(element.JournalEntries.ToArray()),
            } as TEntity,
            Element element => element with {
                Parent = parent ?? element.Parent,
                Timestamp = DateTime.UtcNow,
                State = State.New,
                Tags = new List<string>(),
                Requirements = new List<IValidation>(element.Requirements.ToArray()),
                Attributes = new List<IElementAttribute>(),
                Traits = new List<ITrait>(),
                PowerSources = new List<IPowerSource>(),
                Effects = new List<IEffects>(element.Effects.ToArray()),
                Triggers = new List<ITrigger>(),
                Validations = new List<IValidation>(element.Validations.ToArray()),
            } as TEntity,
            { } => source,
        };
        if (source is not Element s || target is not Element t)
            return target;

        s.Requirements.ToList().ForEach(t.Requirements.Add);
        s.Attributes.ToList().ForEach(a => t.Attributes.Add(a.Clone(t)));
        s.Traits.ToList().ForEach(x => t.Traits.Add(x.Clone(t)));
        s.PowerSources.ToList().ForEach(x => t.PowerSources.Add(x.Clone(t)));
        s.Effects.ToList().ForEach(t.Effects.Add);
        s.Triggers.ToList().ForEach(x => x.Triggers.Add(x.Clone(t)));
        s.Validations.ToList().ForEach(t.Validations.Add);
        return (TEntity)target;
    }
}

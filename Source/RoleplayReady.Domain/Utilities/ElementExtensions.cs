namespace RoleplayReady.Domain.Utilities;

internal static class ElementExtensions {
    public static IElementAttribute<T?> GetAttribute<T>(this IElement element, string name)
        => element.Attributes.OfType<ElementAttribute<T?>>().First(p => p.Name == name);

    public static IElementAttribute<T?>? FindAttribute<T>(this IElement element, string name)
        => element.Attributes.OfType<ElementAttribute<T?>>().FirstOrDefault(p => p.Name == name);

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

    public static TEntity Clone<TEntity>(this TEntity source, IElement? parent = null)
        where TEntity : IEntity {
        var target = source switch {
            Element element => (IEntity)(element with {
                Parent = parent ?? element.Parent,
                Timestamp = DateTime.UtcNow,
                Status = Status.NotReady,
                Tags = new List<string>(),
                Requirements = new List<IValidation>(),
                Attributes = new List<IElementAttribute>(),
                Traits = new List<ITrait>(),
                PowerSources = new List<IPowerSource>(),
                Effects = new List<IEffects>(),
                Triggers = new List<ITrigger>(),
                Validations = new List<IValidation>(),
            }),
            Child child => (IEntity)(child with {
                Parent = parent ?? child.Parent,
            }),
            _ => source,
        };
        if (source is not Element s || target is not Element t)
            return (TEntity)target;

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

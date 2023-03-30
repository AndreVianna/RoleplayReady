namespace RoleplayReady.Domain.Extensions;

internal static class EntityExtensions {
    public static AttributeWithValue<T?>? GetAttribute<T>(this IHasAttributes entity, string name)
        => entity.Attributes.OfType<AttributeWithValue<T?>>().FirstOrDefault(p => p.Name == name);

    public static void ConfigureFeatures(this IHasFeatures entity, Action<IFeaturesBuilder> configure)
        => configure(ElementFeaturesBuilder.For(entity));

    public static IHasFeatures CloneFeaturesFrom(this IHasFeatures target, IHasFeatures source, params string[] excluding) {
        target.Features.Clear();
        foreach (var feature in source.Features.Where(i => !excluding.Contains(i.Name)))
            target.Features.Add(feature with { Parent = target });
        return target;
    }

    public static void ConfigureModifiers(this IHasEffects entity, Action<IModifierBuilder> configure)
        => configure(FeatureModifiersBuilder.For(entity));

    public static TElement Get<TElement>(this RuleSet ruleSet, string name) where TElement : Element
        => ruleSet.Elements.OfType<TElement>().First(e => e.Name == name);
}

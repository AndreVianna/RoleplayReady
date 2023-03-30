namespace RoleplayReady.Domain.Extensions;

internal interface IFeaturesBuilder {
    IFeaturesBuilder Add(string name, string description, Action<IModifierBuilder> build);
}

internal class ElementFeaturesBuilder : IFeaturesBuilder {
    private readonly IHasFeatures _target;

    private ElementFeaturesBuilder(IHasFeatures target) {
        _target = target;
    }

    public static IFeaturesBuilder For(IHasFeatures target) => new ElementFeaturesBuilder(target);

    public IFeaturesBuilder Add(string name, string description, Action<IModifierBuilder> build) {
        var feature = new Feature(_target, name, description);
        build(FeatureModifiersBuilder.For(feature));
        return Add(feature);
    }

    public IFeaturesBuilder Add(string name, Action<IModifierBuilder> build)
        => Add(name, null!, build);

    private IFeaturesBuilder Add(Feature feature) {
        _target.Features.Add(feature);
        return this;
    }
}
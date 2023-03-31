namespace RoleplayReady.Domain.Utilities;

internal static class RuleSetExtensions {
    public static IElement GetElement(this IRuleSet ruleSet, string name)
        => ruleSet.Elements.First(p => p.Name == name);

    public static IElement? FindElement(this IRuleSet ruleSet, string name)
        => ruleSet.Elements.FirstOrDefault(p => p.Name == name);

    public static ISource GetSource(this IRuleSet ruleSet, string abbreviation)
        => ruleSet.Sources.First(p => p.Abbreviation == abbreviation);

    public static ISource? FindSource(this IRuleSet ruleSet, string abbreviation)
        => ruleSet.Sources.FirstOrDefault(p => p.Abbreviation == abbreviation);
}
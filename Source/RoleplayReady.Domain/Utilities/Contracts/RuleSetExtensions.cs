using RolePlayReady.Models.Contracts;

namespace RolePlayReady.Utilities.Contracts;

internal static class RuleSetExtensions {
    public static IComponent GetComponent(this IRuleSet ruleSet, string name)
        => ruleSet.Components.First(p => p.Name == name);

    public static IComponent? FindComponent(this IRuleSet ruleSet, string name)
        => ruleSet.Components.FirstOrDefault(p => p.Name == name);

    public static ISource GetSource(this IRuleSet ruleSet, string abbreviation)
        => ruleSet.Sources.First(p => p.Abbreviation == abbreviation);

    public static ISource? FindSource(this IRuleSet ruleSet, string abbreviation)
        => ruleSet.Sources.FirstOrDefault(p => p.Abbreviation == abbreviation);
}
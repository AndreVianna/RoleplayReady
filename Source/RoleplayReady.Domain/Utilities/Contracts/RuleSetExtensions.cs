namespace RolePlayReady.Utilities.Contracts;

internal static class RuleSetExtensions {
    public static INode GetComponent(this IRuleSet ruleSet, string name)
        => ruleSet.Children.OfType<IEntity>().First(p => p.Name == name);

    public static INode? FindComponent(this IRuleSet ruleSet, string name)
        => ruleSet.Children.OfType<IEntity>().FirstOrDefault(p => p.Name == name);

    //public static ISource GetSource(this IRuleSet ruleSet, string abbreviation)
    //    => ruleSet.Sources.First(p => p.Abbreviation == abbreviation);

    //public static ISource? FindSource(this IRuleSet ruleSet, string abbreviation)
    //    => ruleSet.Sources.FirstOrDefault(p => p.Abbreviation == abbreviation);
}
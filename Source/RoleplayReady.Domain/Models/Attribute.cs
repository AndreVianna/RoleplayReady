namespace RolePlayReady.Models;

public record Attribute : IAttribute {
    public Attribute() { }

    [SetsRequiredMembers]
    public Attribute(Type dataType, IRuleSet ruleSet, string abbreviation, string name, string description) {
        RuleSet = ruleSet;
        Abbreviation = abbreviation;
        Name = name;
        Description = description;
        DataType = dataType;
    }

    public required IRuleSet RuleSet { get; init; }
    public string EntityType => nameof(Attribute);
    public required string Abbreviation { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string FullName => $"<{RuleSet.Abbreviation}:{EntityType}>{Name}({Abbreviation}):{DataType.Name}";
    public required Type DataType { get; init; }
}
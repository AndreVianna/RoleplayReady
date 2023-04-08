namespace RolePlayReady.Models;

public record RuleSet : Entity, IOwned, IRuleSet {
    private readonly string _shortName1 = string.Empty;
    public string Owner { get; init; } = "System";

    public new required string ShortName {
        get => _shortName1;
        init => _shortName1 = Throw.IfNullOrWhiteSpaces(value);
    }

    public required IList<IAttributeDefinition> AttributeDefinitions { get; init; } = new List<IAttributeDefinition>();
}
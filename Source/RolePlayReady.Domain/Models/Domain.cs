namespace RolePlayReady.Models;

public record Domain : Entity {
    public IList<IAttributeDefinition> AttributeDefinitions { get; init; } = new List<IAttributeDefinition>();

    public override ValidationResult Validate() {
        var result = base.Validate();
        result += AttributeDefinitions.ForEach(item => item.IsNotNull().And.IsValid()).Result;
        return result;
    }
}
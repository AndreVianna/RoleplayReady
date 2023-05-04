namespace RolePlayReady.Handlers.Sphere;

public record Sphere : Persisted {
    public ICollection<Base> Components { get; init; } = new List<Base>();
    public ICollection<AttributeDefinition> AttributeDefinitions { get; init; } = new List<AttributeDefinition>();

    public override ValidationResult ValidateSelf() {
        var result = base.ValidateSelf();
        result += Components.ForEach(item => item.IsRequired().And().IsValid()).Result;
        result += AttributeDefinitions.ForEach(item => item.IsRequired().And().IsValid()).Result;
        return result;
    }
}
namespace RolePlayReady.Handlers.Sphere;

public record Sphere : Persisted {
    public ICollection<Base> Components { get; init; } = new List<Base>();
    public ICollection<AttributeDefinition> AttributeDefinitions { get; init; } = new List<AttributeDefinition>();

    public override ValidationResult ValidateSelf(bool negate = false) {
        var result = base.ValidateSelf();
        result += Components!.CheckIfEach(item => item.IsRequired().And().IsValid()).Result;
        result += AttributeDefinitions!.CheckIfEach(item => item.IsRequired().And().IsValid()).Result;
        return result;
    }
}
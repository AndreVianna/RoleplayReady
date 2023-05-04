namespace RolePlayReady.Handlers.Sphere;

public record Sphere : Persisted {
    public ICollection<Base> Components { get; init; } = new List<Base>();
    public ICollection<AttributeDefinition> AttributeDefinitions { get; init; } = new List<AttributeDefinition>();

    public override ICollection<ValidationError> Validate() {
        var result = (ValidationResult)base.Validate();
        result += Components!.ForEach(item => item.IsNotNull().And.IsValid());
        result += AttributeDefinitions!.ForEach(item => item.IsNotNull().And.IsValid());
        return result.Errors;
    }
}
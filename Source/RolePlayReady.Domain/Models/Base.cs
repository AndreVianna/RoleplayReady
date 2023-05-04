namespace RolePlayReady.Models;

public record Base : IBase, IValidatable {
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string? ShortName { get; init; }

    public ICollection<string> Tags { get; init; } = new List<string>();

    public virtual ICollection<ValidationError> Validate() {
        var result = ValidationResult.Success();
        result += Name.IsNotNull()
                      .And.IsNotEmptyOrWhiteSpace()
                      .And.MaximumLengthIs(Validation.Name.MaximumLength).Errors;
        result += Description.IsNotNull()
                             .And.IsNotEmptyOrWhiteSpace()
                             .And.MaximumLengthIs(Validation.Description.MaximumLength).Errors;
        result += ShortName.IsNullOr()
                           .IsNotEmptyOrWhiteSpace()
                           .And.MaximumLengthIs(Validation.ShortName.MaximumLength).Errors;
        result += Tags!.ForEach(item => item.IsNotNull()
                                           .And.IsNotEmptyOrWhiteSpace()
                                           .And.MaximumLengthIs(Validation.Tag.MaximumLength).Errors);
        return result.Errors;
    }
}
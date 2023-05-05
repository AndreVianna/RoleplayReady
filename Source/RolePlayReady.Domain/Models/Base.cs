using System.Extensions;
using System.Validation;

namespace RolePlayReady.Models;

public record Base : IBase, IValidatable {
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string? ShortName { get; init; }

    public ICollection<string> Tags { get; init; } = new List<string>();

    public virtual ValidationResult ValidateSelf() {
        var result = ValidationResult.Success();
        result += Name.IsRequired()
            .And().IsNotEmptyOrWhiteSpace()
            .And().MaximumLengthIs(Validation.Name.MaximumLength).Result;
        result += Description.IsRequired()
            .And().IsNotEmptyOrWhiteSpace()
            .And().MaximumLengthIs(Validation.Description.MaximumLength).Result;
        result += ShortName.IsOptional()
            .And().IsNotEmptyOrWhiteSpace()
            .And().MaximumLengthIs(Validation.ShortName.MaximumLength).Result;
        result += Tags!.ForEach(item =>
            item.IsRequired()
                .And().IsNotEmptyOrWhiteSpace()
                .And().MaximumLengthIs(Validation.Tag.MaximumLength)).Result;
        return result;
    }
}
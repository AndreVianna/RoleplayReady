namespace RoleplayReady.Domain.Models.Validations;

public record Validation : IValidation {
    public Validation() { }

    [SetsRequiredMembers]
    public Validation(Func<IElement, bool> validate, string errorMessage) {
        Validate = validate;
        ErrorMessage = errorMessage;
    }

    public required Func<IElement, bool> Validate { get; init; }
    public required string ErrorMessage { get; init; }
}
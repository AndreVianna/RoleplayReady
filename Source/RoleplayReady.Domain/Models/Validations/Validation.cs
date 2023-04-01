namespace RoleplayReady.Domain.Models.Validations;

public record Validation : IValidation {
    public Validation() { }

    [SetsRequiredMembers]
    public Validation(Func<IElement, bool> validate, string failureMessage) {
        Validate = validate;
        FailureMessage = failureMessage;
    }

    public required Func<IElement, bool> Validate { get; init; }
    public required string FailureMessage { get; init; }
}
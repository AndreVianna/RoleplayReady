namespace RoleplayReady.Domain.Validations;

public record Validation : IValidation {
    public Validation() { }

    [SetsRequiredMembers]
    public Validation(Func<IEntity, bool> validate, string failureMessage) {
        Validate = validate;
        FailureMessage = failureMessage;
    }

    public required Func<IEntity, bool> Validate { get; init; }
    public required string FailureMessage { get; init; }
}
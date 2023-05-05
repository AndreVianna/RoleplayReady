namespace System.Validation.Commands.Object;

public sealed class IsNotNull : ValidationCommand<object?> {
    public IsNotNull(object? subject, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
    }

    public override ValidationResult Validate()
        => Subject is null
            ? AddError(CannotBeNull)
            : Validation;
}
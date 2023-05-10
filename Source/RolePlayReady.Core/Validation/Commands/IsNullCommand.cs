namespace System.Validation.Commands;

public sealed class IsNullCommand : ValidationCommand {
    public IsNullCommand(string source)
        : base(source) {
        ValidationErrorMessage = MustBeNull;
    }

    public override ValidationResult Validate(object? subject) {
        if (subject is null) return ValidationResult.Success();
        return ValidationResult.Invalid(ValidationErrorMessage, Source, GetErrorMessageArguments(subject));
    }

    public override ValidationResult Negate(object? subject) {
        if (subject is not null) return ValidationResult.Success();
        return ValidationResult.Invalid(InvertMessage(ValidationErrorMessage), Source, GetErrorMessageArguments(subject));
    }
}
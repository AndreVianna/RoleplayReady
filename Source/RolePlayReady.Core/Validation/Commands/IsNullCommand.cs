namespace System.Validation.Commands;

public sealed class IsNullCommand : ValidationCommand {
    public IsNullCommand(string source)
        : base(source) {
        ValidationErrorMessage = MustBeNull;
    }

    public override ValidationResult Validate(object? subject) => subject is null
            ? ValidationResult.Success()
            : ValidationResult.Invalid(ValidationErrorMessage, Source, GetErrorMessageArguments(subject));

    public override ValidationResult Negate(object? subject) => subject is null
            ? ValidationResult.Success()
            : ValidationResult.Invalid(InvertMessage(ValidationErrorMessage), Source, GetErrorMessageArguments(subject));
}
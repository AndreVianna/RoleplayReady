namespace System.Validation.Commands;

public sealed class IsNullCommand
    : ValidationCommand<object?> {

    public IsNullCommand(object? subject, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s is null;
        ValidationErrorMessage = MustBeNull;
    }
}
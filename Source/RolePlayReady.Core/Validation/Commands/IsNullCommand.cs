namespace System.Validation.Commands;

public sealed class IsNullCommand : ValidationCommand {
    public IsNullCommand(string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s is null;
        ValidationErrorMessage = MustBeNull;
    }
}
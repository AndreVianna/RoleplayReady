namespace System.Validation.Commands;

public sealed class IsEmptyOrWhiteSpaceCommand
    : ValidationCommand<string> {
    public IsEmptyOrWhiteSpaceCommand(string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s.Trim().Length == 0;
        ValidationErrorMessage = MustBeEmptyOrWhitespace;
    }
}
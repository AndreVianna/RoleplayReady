namespace System.Validation.Commands;

public sealed class IsEmptyOrWhiteSpaceCommand
    : ValidationCommand {
    public IsEmptyOrWhiteSpaceCommand(string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => ((string)s!).Trim().Length == 0;
        ValidationErrorMessage = MustBeEmptyOrWhitespace;
    }
}
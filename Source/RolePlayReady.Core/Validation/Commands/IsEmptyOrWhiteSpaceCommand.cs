namespace System.Validation.Commands;

public sealed class IsEmptyOrWhiteSpaceCommand
    : ValidationCommand<string> {

    public IsEmptyOrWhiteSpaceCommand(string subject, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.Trim().Length == 0;
        ValidationErrorMessage = MustBeEmptyOrWhitespace;
    }
}
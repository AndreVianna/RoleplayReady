namespace System.Validation.Commands;

public sealed class LengthIsCommand
    : ValidationCommand<string> {
    public LengthIsCommand(int length, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s.Length == length;
        ValidationErrorMessage = MustHaveALengthOf;
        Arguments = SetArguments(length);
    }
}
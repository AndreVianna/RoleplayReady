namespace System.Validation.Commands;

public sealed class LengthIsCommand
    : ValidationCommand<string> {

    public LengthIsCommand(string subject, int length, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.Length == length;
        ValidationErrorMessage = MustHaveALengthOf;
        Arguments = SetArguments(length, subject.Length);
    }
}
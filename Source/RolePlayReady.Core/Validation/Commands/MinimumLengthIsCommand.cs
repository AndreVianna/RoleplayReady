namespace System.Validation.Commands;

public sealed class MinimumLengthIsCommand : ValidationCommand<string> {

    public MinimumLengthIsCommand(string subject, int length, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.Length >= length;
        ValidationErrorMessage = MustHaveAMinimumLengthOf;
        Arguments = SetArguments(length, subject.Length);
    }
}
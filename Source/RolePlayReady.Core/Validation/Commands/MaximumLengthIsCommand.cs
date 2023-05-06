namespace System.Validation.Commands;

public sealed class MaximumLengthIsCommand
    : ValidationCommand<string> {

    public MaximumLengthIsCommand(string subject, int length, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.Length <= length;
        ValidationErrorMessage = MustHaveAMaximumLengthOf;
        Arguments = SetArguments(length, subject.Length);
    }
}
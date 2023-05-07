namespace System.Validation.Commands;

public sealed class MaximumLengthIsCommand
    : ValidationCommand<string> {
    public MaximumLengthIsCommand(int length, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s.Length <= length;
        ValidationErrorMessage = MustHaveAMaximumLengthOf;
        Arguments = SetArguments(length);
    }
}
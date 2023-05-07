namespace System.Validation.Commands;

public sealed class MinimumLengthIsCommand : ValidationCommand<string> {
    public MinimumLengthIsCommand(int length, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s.Length >= length;
        ValidationErrorMessage = MustHaveAMinimumLengthOf;
        Arguments = SetArguments(length);
    }
}
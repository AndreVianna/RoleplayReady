namespace System.Validation.Commands;

public sealed class LengthIsAtLeastCommand : ValidationCommand {
    public LengthIsAtLeastCommand(int length, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => ((string)s!).Length >= length;
        ValidationErrorMessage = MustHaveAMinimumLengthOf;
        GetArguments = s => new object?[] { length, ((string)s!).Length };
    }
}
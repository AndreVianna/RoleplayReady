namespace System.Validation.Commands;

public sealed class MaximumLengthIsCommand
    : ValidationCommand {
    public MaximumLengthIsCommand(int length, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => ((string)s!).Length <= length;
        ValidationErrorMessage = MustHaveAMaximumLengthOf;
        GetArguments = s => new object?[] { length, ((string)s!).Length };
    }
}
namespace System.Validation.Commands;

public sealed class LengthIsCommand
    : ValidationCommand {
    public LengthIsCommand(int length, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => ((string)s!).Length == length;
        ValidationErrorMessage = MustHaveALengthOf;
        GetArguments = s => new object?[] { length, ((string)s!).Length };
    }
}
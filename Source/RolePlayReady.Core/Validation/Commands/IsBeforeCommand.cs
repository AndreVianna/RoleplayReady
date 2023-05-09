namespace System.Validation.Commands;

public sealed class IsBeforeCommand
    : ValidationCommand {
    public IsBeforeCommand(DateTime @event, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = dt => (DateTime)dt! < @event;
        ValidationErrorMessage = MustBeBefore;
        GetErrorMessageArguments = dt => new object?[] { @event, (DateTime)dt! };
    }
}
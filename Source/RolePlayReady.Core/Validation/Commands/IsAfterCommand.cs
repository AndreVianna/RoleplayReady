namespace System.Validation.Commands;

public sealed class IsAfterCommand
    : ValidationCommand {
    public IsAfterCommand(DateTime @event, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = dt => (DateTime)dt! > @event;
        ValidationErrorMessage = MustBeAfter;
        GetArguments = dt => new object?[] { @event, (DateTime)dt! };
    }
}
namespace System.Validation.Commands;

public sealed class IsBeforeCommand
    : ValidationCommand<DateTime> {
    public IsBeforeCommand(DateTime threshold, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s.CompareTo(threshold) < 0;
        ValidationErrorMessage = MustBeBefore;
        Arguments = SetArguments(threshold);
    }
}
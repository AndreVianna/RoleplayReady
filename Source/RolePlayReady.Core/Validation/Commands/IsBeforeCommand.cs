namespace System.Validation.Commands;

public sealed class IsBeforeCommand
    : ValidationCommand<DateTime> {

    public IsBeforeCommand(DateTime subject, DateTime threshold, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.CompareTo(threshold) < 0;
        ValidationErrorMessage = MustBeBefore;
        Arguments = SetArguments(threshold, subject);
    }
}
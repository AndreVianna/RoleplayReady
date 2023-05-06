namespace System.Validation.Commands;

public sealed class IsAfterCommand
    : ValidationCommand<DateTime> {

    public IsAfterCommand(DateTime subject, DateTime threshold, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.CompareTo(threshold) > 0;
        ValidationErrorMessage = MustBeAfter;
        Arguments = SetArguments(threshold, subject);
    }
}
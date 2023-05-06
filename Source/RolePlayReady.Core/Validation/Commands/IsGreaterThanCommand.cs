namespace System.Validation.Commands;

public sealed class IsGreaterThanCommand<TValue>
    : ValidationCommand<TValue>
    where TValue : IComparable<TValue> {

    public IsGreaterThanCommand(TValue subject, TValue threshold, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.CompareTo(threshold) > 0;
        ValidationErrorMessage = MustBeGraterThan;
        Arguments = SetArguments(threshold, subject);
    }
}
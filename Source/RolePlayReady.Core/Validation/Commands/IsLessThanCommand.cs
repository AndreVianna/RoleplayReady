namespace System.Validation.Commands;

public sealed class IsLessThanCommand<TValue>
    : ValidationCommand<TValue>
    where TValue : IComparable<TValue> {

    public IsLessThanCommand(TValue subject, TValue threshold, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.CompareTo(threshold) < 0;
        ValidationErrorMessage = MustBeLessThan;
        ValidationArguments = AddArguments(threshold);
    }
}
namespace System.Validation.Commands;

public sealed class IsLessThanCommand<TValue>
    : ValidationCommand
    where TValue : IComparable<TValue> {
    public IsLessThanCommand(TValue threshold, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = v => ((TValue)v!).CompareTo(threshold) < 0;
        ValidationErrorMessage = MustBeLessThan;
        GetErrorMessageArguments = v => new[] { threshold, v };
    }
}
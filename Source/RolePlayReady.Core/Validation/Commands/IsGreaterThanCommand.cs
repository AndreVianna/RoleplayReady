namespace System.Validation.Commands;

public sealed class IsGreaterThanCommand<TValue>
    : ValidationCommand
    where TValue : IComparable<TValue> {
    public IsGreaterThanCommand(TValue threshold, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = v => ((TValue)v!).CompareTo(threshold) > 0;
        ValidationErrorMessage = MustBeGraterThan;
        GetArguments = v => new[] { threshold, v };
    }
}
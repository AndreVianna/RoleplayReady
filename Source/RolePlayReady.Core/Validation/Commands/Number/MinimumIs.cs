namespace System.Validation.Commands.Number;

public sealed class MinimumIs<TValue> : ValidationCommand<TValue>
    where TValue : struct, IComparable<TValue> {
    private readonly TValue _threshold;

    public MinimumIs(TValue subject, TValue threshold, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        _threshold = threshold;
    }

    public override ValidationResult Validate()
        => Subject.CompareTo(_threshold) < 0
            ? AddError(CannotBeLessThan, _threshold, Subject)
            : Validation;
}
namespace System.Validation.Commands.Number;

public sealed class MaximumIs<TValue> : ValidationCommand<TValue>
    where TValue : struct, IComparable<TValue> {
    private readonly TValue _threshold;

    public MaximumIs(TValue subject, TValue threshold, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        _threshold = threshold;
    }

    public override ValidationResult Validate()
        => Subject.CompareTo(_threshold) > 0
            ? AddError(CannotBeGreaterThan, _threshold, Subject)
            : Validation;
}
namespace System.Validation.Commands.Number;

public sealed class IsEqualTo<TValue> : ValidationCommand<TValue>
    where TValue : struct, IComparable<TValue> {
    private readonly TValue _value;

    public IsEqualTo(TValue subject, TValue value, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        _value = value;
    }

    public override ValidationResult Validate()
        => Subject.CompareTo(_value) != 0
            ? AddError(MustBeEqualTo, _value, Subject)
            : Validation;
}

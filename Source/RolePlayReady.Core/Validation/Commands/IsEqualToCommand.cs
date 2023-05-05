namespace System.Validation.Commands;

public sealed class IsEqualToCommand<TValue>
    : ValidationCommand<TValue> {

    public IsEqualToCommand(TValue subject, TValue value, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s is not null && s.Equals(value);
        NegateAs = s => s is not null && !s.Equals(value);
        ValidationErrorMessage = MustBeEqualTo;
        ValidationArguments = AddArguments(value);
    }
}

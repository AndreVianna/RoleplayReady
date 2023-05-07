namespace System.Validation.Commands;

public sealed class IsEqualToCommand<TValue>
    : ValidationCommand{
    public IsEqualToCommand(TValue value, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s!.Equals(value);
        ValidationErrorMessage = MustBeEqualTo;
        GetArguments = o => new[] { value, o };
    }
}

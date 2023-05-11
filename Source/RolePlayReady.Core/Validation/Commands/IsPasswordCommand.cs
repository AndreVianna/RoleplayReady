namespace System.Validation.Commands;

public sealed class IsPasswordCommand
    : ValidationCommand {
    private readonly IPasswordPolicy _policy;

    public IsPasswordCommand(IPasswordPolicy policy, string source)
        : base(source) {
        _policy = policy;
    }

    public override ValidationResult Validate(object? subject) {
        if (subject is not string s || _policy.TryValidate(s, out var isValidEmailResult))
            return ValidationResult.Success();
        var result = ValidationResult.Invalid(MustBeAValidPassword, Source, GetErrorMessageArguments(subject));
        return isValidEmailResult.Errors.Aggregate(result, (current, error) => current + error);
    }
}
namespace System.Validation.Commands;

public sealed class IsPasswordCommand
    : ValidationCommand {
    private readonly IPasswordPolicy _policy;

    public IsPasswordCommand(string source, IPasswordPolicy policy)
        : base(source) {
        _policy = policy;
    }

    public override ValidationResult Validate(object? subject) {
        if (subject is not string s || _policy.TryValidate(s, out var isValidEmail))
            return ValidationResult.Success();
        var result = ValidationResult.Invalid(MustBeAValidPassword, Source, GetErrorMessageArguments(subject));
        return isValidEmail.Errors.Aggregate(result, (current, error) => current + error);
    }

    public override ValidationResult Negate(object? subject) {
        if (subject is not string s || !_policy.TryValidate(s, out var isValidEmail))
            return ValidationResult.Success();
        var result = ValidationResult.Invalid(InvertMessage(MustBeAValidPassword), Source, GetErrorMessageArguments(subject));
        return isValidEmail.Errors.Aggregate(result, (current, error) => current + error);
    }
}
namespace System.Validation.Commands;

public sealed class IsPasswordCommand
    : ValidationCommand {
    private readonly IPasswordPolicy _policy;

    public IsPasswordCommand(IPasswordPolicy policy, string source)
        : base(source) {
        _policy = policy;
    }

    public override ValidationResult Validate(object? subject) {
        if (subject is not string password) return ValidationResult.Success();
        var policyResult = _policy.Enforce(password);
        if (policyResult.IsSuccess) return ValidationResult.Success();
        var result = ValidationResult.Invalid(MustBeAValidPassword, Source, GetErrorMessageArguments(subject));
        return policyResult.Errors.Aggregate(result, (current, error) => current + error);
    }
}
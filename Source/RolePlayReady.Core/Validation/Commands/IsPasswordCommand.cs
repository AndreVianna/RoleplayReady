namespace System.Validation.Commands;

public sealed class IsPasswordCommand(IPasswordPolicy policy, string source)
        : ValidationCommand(source) {
    public override ValidationResult Validate(object? subject) {
        if (subject is not string password)
            return ValidationResult.Success();
        var policyResult = policy.Enforce(password);
        if (policyResult.IsSuccess)
            return ValidationResult.Success();
        var result = ValidationResult.Invalid(MustBeAValidPassword, Source, GetErrorMessageArguments(subject));
        return policyResult.Errors.Aggregate(result, (current, error) => current + error);
    }
}
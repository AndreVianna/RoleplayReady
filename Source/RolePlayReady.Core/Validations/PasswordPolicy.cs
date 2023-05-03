using System.Validations.Extensions;

namespace System.Validations;

public class PasswordPolicy : IPasswordPolicy {
    public ValidationResult Validate(object? input, [CallerArgumentExpression(nameof(input))] string? source = null) {
        var result = ValidationResult.Success();
        result += input.IsNotNull(source).And.IsOfType<string>().Result;
        return result;
    }
}
namespace System.Validation.Commands;

public sealed class IsValidCommand(string source)
        : ValidationCommand(source) {
    public override ValidationResult Validate(object? subject) {
        var result = ValidationResult.Success();
        if (subject is not IValidatable v)
            return result;
        var validation = v.Validate();
        foreach (var error in validation.Errors) {
            error.Arguments[0] = $"{Source}.{error.Arguments[0]}";
            result += error;
        }

        return result;
    }
}
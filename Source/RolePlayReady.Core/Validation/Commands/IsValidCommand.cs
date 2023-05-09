namespace System.Validation.Commands;

public sealed class IsValidCommand
    : ValidationCommand {
    public IsValidCommand(string source, ValidationResult? validation = null)
        : base(source, validation) {
    }

    public override ValidationResult Validate(object? subject) {
        if (subject is not IValidatable v) return Validation;
        var validation = v.ValidateSelf();
        foreach (var error in validation.Errors) {
            error.Arguments[0] = $"{Source}.{error.Arguments[0]}";
            AddError(error);
        }

        return Validation;
    }
}
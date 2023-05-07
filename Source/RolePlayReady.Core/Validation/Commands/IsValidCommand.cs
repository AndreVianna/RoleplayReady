namespace System.Validation.Commands;

public sealed class IsValidCommand
    : ValidationCommand<IValidatable> {
    public IsValidCommand(string source, ValidationResult? validation = null)
        : base(source, validation) {
    }

    public override ValidationResult Validate(IValidatable? subject) {
        if (subject is null) return Validation;
        var validation = subject.ValidateSelf();
        foreach (var error in validation.Errors) {
            error.Arguments[0] = $"{Source}.{error.Arguments[0]}";
            AddError(error);
        }

        return Validation;
    }

    public override ValidationResult Negate(IValidatable? subject) {
        if (subject is null) return Validation;
        var validation = subject.ValidateSelf(true);
        foreach (var error in validation.Errors) {
            error.Arguments[0] = $"{Source}.{error.Arguments[0]}";
            AddError(error);
        }

        return Validation;
    }
}
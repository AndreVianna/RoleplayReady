namespace System.Validation.Commands;

public sealed class IsValidCommand
    : ValidationCommand<IValidatable> {

    public IsValidCommand(IValidatable subject, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
    }

    public override ValidationResult Validate() {
        if (Subject is null) return Validation;
        var validation = Subject.ValidateSelf();
        foreach (var error in validation.Errors) {
            error.Arguments[0] = $"{Source}.{error.Arguments[0]}";
            AddError(error);
        }

        return Validation;
    }

    public override ValidationResult Negate() {
        if (Subject is null) return Validation;
        var validation = Subject.ValidateSelf(true);
        foreach (var error in validation.Errors) {
            error.Arguments[0] = $"{Source}.{error.Arguments[0]}";
            AddError(error);
        }

        return Validation;
    }
}
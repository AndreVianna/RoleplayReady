using System.Validation.Abstractions;

namespace System.Validation.Commands.Object;

public sealed class IsValid : ValidationCommand<IValidatable> {
    public IsValid(IValidatable subject, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
    }

    public override ValidationResult Validate() {
        var validation = Subject.ValidateSelf();
        foreach (var error in validation.Errors) {
            error.Arguments[0] = $"{Source}.{error.Arguments[0]}";
            AddError(error);
        }

        return Validation;
    }
}
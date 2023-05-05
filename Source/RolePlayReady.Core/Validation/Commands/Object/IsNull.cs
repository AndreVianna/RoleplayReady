namespace System.Validation.Commands.Object;

public sealed class IsNull : ValidationCommand<object?> {
    public IsNull(object? subject, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
    }

    public override ValidationResult Validate()
        => Subject is not null
            ? AddError(MustBeNull)
            : Validation;
}
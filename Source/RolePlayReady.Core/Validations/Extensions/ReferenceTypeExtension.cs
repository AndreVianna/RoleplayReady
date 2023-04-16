namespace System.Validations.Extensions;

public static class ReferenceTypeExtension {
    public static IValidatableValidations ValueIs(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new ValidatableValidation(subject, source);
    public static IReferenceTypeValidations ValueIs(this object? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new ReferenceTypeValidation(subject, source);
}
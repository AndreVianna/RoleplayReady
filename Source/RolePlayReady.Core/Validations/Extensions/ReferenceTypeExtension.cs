namespace System.Validations.Extensions;

public static class ReferenceTypeExtension {
    public static IValidatableObjectValidations ValueIs(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new ValidatableObjectValidation(subject, source);
    public static IReferenceTypeValidations ValueIs(this object? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new ReferenceTypeValidation(subject, source);
}
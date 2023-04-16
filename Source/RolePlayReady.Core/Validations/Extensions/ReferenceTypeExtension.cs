namespace System.Validations.Extensions;

public static class ReferenceTypeExtension {
    public static IValidatableTypeValidation Is(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new ValidatableValidation(subject, source);
    public static IReferenceTypeValidation Is(this object? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new ReferenceTypeValidation(subject, source);
}
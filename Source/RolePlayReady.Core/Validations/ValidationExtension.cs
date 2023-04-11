namespace System.Validations;

public static class ValidationExtension {
    public static IStringValidators Is(this string? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => StringValidationBuilder.For(subject, source);
    public static IStringCollectionValidators Is(this IList<string> subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => StringCollectionValidationBuilder.For(subject, source);
    public static IStringValidationConnector<IStringCollectionValidators> AreAll(this IList<string> subject, Func<IStringValidators, IStringValidationConnector<IStringValidators>> validate, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => StringCollectionValidationBuilder.For(subject, source).AreAll(validate);
}
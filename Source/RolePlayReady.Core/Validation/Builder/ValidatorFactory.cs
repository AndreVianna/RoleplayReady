using ValidationResult = System.Results.ValidationResult;

namespace System.Validation.Builder;

public static class ValidatorFactory {
    private static ValidationResult AllowNull(bool allowNull, object? subject, string source)
        => (subject is null && !allowNull)
            ? new[] { new ValidationError(InvertMessage(MustBeNull), source) }
            : ValidationResult.Success();

    public static Connectors<ICollection<TSubject?>, CollectionValidators<TSubject>> Create<TSubject>(ICollection<TSubject?>? subject, string source)
        => new(new(subject, source, AllowNull(false, subject, source)));

    public static IConnectors<ICollection<TSubject?>, CollectionValidators<TSubject>> Create<TSubject>(ICollection<TSubject?>? subject, string source, Func<TSubject?, ITerminator> validateItem)
        => new CollectionValidators<TSubject>(subject, source, AllowNull(false, subject, source)).ForEach(validateItem);

    public static Connectors<DateTime?, DateTimeValidators> Create(bool allowNull, DateTime? subject, string source)
        => new(new(subject, source, AllowNull(allowNull, subject, source)));

    public static Connectors<decimal?, DecimalValidators> Create(bool allowNull, decimal? subject, string source)
        => new(new(subject, source, AllowNull(allowNull, subject, source)));

    public static Connectors<IDictionary<TSubjectKey, TSubjectValue?>, DictionaryValidators<TSubjectKey, TSubjectValue>> Create<TSubjectKey, TSubjectValue>(IDictionary<TSubjectKey, TSubjectValue?>? subject, string source)
        where TSubjectKey : notnull
    => new(new(subject, source, AllowNull(false, subject, source)));

    public static IConnectors<IDictionary<TSubjectKey, TSubjectValue?>, DictionaryValidators<TSubjectKey, TSubjectValue>> Create<TSubjectKey, TSubjectValue>(IDictionary<TSubjectKey, TSubjectValue?>? subject, string source, Func<TSubjectValue?, ITerminator> validateValue)
        where TSubjectKey : notnull
        => new DictionaryValidators<TSubjectKey, TSubjectValue>(subject, source, AllowNull(false, subject, source)).ForEach(validateValue);

    public static Connectors<int?, IntegerValidators> Create(bool allowNull, int? subject, string source)
        => new(new(subject, source, AllowNull(allowNull, subject, source)));

    public static Connectors<object?, ObjectValidators> Create(bool allowNull, object? subject, string source)
        => new(new(subject, source, AllowNull(allowNull, subject, source)));

    public static Connectors<string?, StringValidators> Create(bool allowNull, string? subject, string source)
        => new(new(subject, source, AllowNull(allowNull, subject, source)));

    public static Connectors<Type?, TypeValidators> Create(Type? subject, string source)
        => new(new(subject, source, AllowNull(false, subject, source)));

    public static Connectors<IValidatable?, ValidatableValidators> Create(bool allowNull, IValidatable? subject, string source)
        => new(new(subject, source, AllowNull(allowNull, subject, source)));
}

using System.Validation.Abstractions;
using System.Validation.Commands.Collection;
using System.Validation.Commands.Number;
using System.Validation.Commands.Object;
using System.Validation.Commands.Text;

namespace System.Validation.Commands;

public sealed class ValidationCommandFactory {
    public static ValidationCommandFactory<TSubject> For<TSubject>(TSubject? subject, string source, ValidationResult? validation = null)
        => ValidationCommandFactory<TSubject>.For(subject, source, validation);
}

public sealed class ValidationCommandFactory<TSubject> {
    private readonly TSubject? _subject;
    private readonly string _source;
    private readonly ValidationResult? _validation;

    private ValidationCommandFactory(TSubject? subject, string source, ValidationResult? validation = null) {
        _subject = subject;
        _source = source;
        _validation = validation;
    }

    public static ValidationCommandFactory<TSubject> For(TSubject? subject, string source, ValidationResult? validation = null)
        => new(subject, source, validation);

    public IValidationCommand Create(string validator, params object?[] arguments)
        => _subject switch {
            null => CreateObjectValidator(_subject, validator),
            IValidatable value => CreateObjectValidator(value, validator),
            int value => CreateNumberValidator(value, validator, arguments),
            decimal value => CreateNumberValidator(value, validator, arguments),
            string value => CreateStringValidator(value, validator, arguments),
            ICollection<int?> value => CreateCollectionValidator(value, validator, arguments),
            ICollection<decimal?> value => CreateCollectionValidator(value, validator, arguments),
            ICollection<string?> value => CreateCollectionValidator(value, validator, arguments),
            IDictionary<string, int?> value => CreateDictionaryValidator(value, validator, arguments),
            IDictionary<string, decimal?> value => CreateDictionaryValidator(value, validator, arguments),
            IDictionary<string, string?> value => CreateDictionaryValidator(value, validator, arguments),
            _ => throw new InvalidOperationException($"Unsupported validator data type '{typeof(TSubject).GetName()}'."),
        };

    private IValidationCommand CreateObjectValidator(object? subject, string? validator) => validator switch {
        nameof(IsNotNull) => new IsNotNull(subject, _source, _validation),
        nameof(IsNull) => new IsNull(subject, _source, _validation),
        nameof(IsValid) => new IsValid((IValidatable)subject!, _source, _validation),
        _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
    };

    private IValidationCommand CreateNumberValidator<TValue>(TValue subject, string validator, IReadOnlyList<object?> arguments)
        where TValue : struct, IComparable<TValue> {
        return validator switch {
            nameof(IsLessThan<TValue>) => new IsLessThan<TValue>(subject, GetLimit(), _source, _validation),
            nameof(MinimumIs<TValue>) => new MinimumIs<TValue>(subject, GetLimit(), _source, _validation),
            nameof(IsEqualTo<TValue>) => new IsEqualTo<TValue>(subject, GetLimit(), _source, _validation),
            nameof(MaximumIs<TValue>) => new MaximumIs<TValue>(subject, GetLimit(), _source, _validation),
            nameof(IsGreaterThan<TValue>) => new IsGreaterThan<TValue>(subject, GetLimit(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };

        TValue GetLimit() => Ensure.ArgumentExistsAndIsOfType<TValue>(validator, arguments, 0);
    }

    private IValidationCommand CreateStringValidator(string subject, string? validator, IReadOnlyList<object?> arguments) {
        return validator switch {
            nameof(MinimumLengthIs) => new MinimumLengthIs(subject, GetLength(), _source, _validation),
            nameof(MaximumLengthIs) => new MaximumLengthIs(subject, GetLength(), _source, _validation),
            nameof(LengthIs) => new LengthIs(subject, GetLength(), _source, _validation),
            nameof(IsOneOf) => new IsOneOf(subject, GetList(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };

        int GetLength() => Ensure.ArgumentExistsAndIsOfType<int>(validator, arguments, 0);
        string?[] GetList() => Ensure.ArgumentsAreAllOfTypeOrDefault<string>(validator, arguments).ToArray();
    }

    private IValidationCommand CreateCollectionValidator<TItem>(ICollection<TItem?> subject, string validator, IReadOnlyList<object?> arguments) {
        return validator switch {
            nameof(IsNotEmpty<int>) => new IsNotEmpty<TItem>(subject, _source, _validation),
            nameof(MinimumCountIs<int>) => new MinimumCountIs<TItem>(subject, GetCount(), _source, _validation),
            nameof(MaximumCountIs<int>) => new MaximumCountIs<TItem>(subject, GetCount(), _source, _validation),
            nameof(CountIs<int>) => new CountIs<TItem>(subject, GetCount(), _source, _validation),
            nameof(Contains<int>) => new Contains<TItem>(subject, GetItem(), _source, _validation),
            nameof(NotContains<int>) => new NotContains<TItem>(subject, GetItem(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };

        int GetCount() => Ensure.ArgumentExistsAndIsOfType<int>(validator, arguments, 0);
        TItem? GetItem() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TItem>(validator, arguments, 0);
    }

    private IValidationCommand CreateDictionaryValidator<TKey, TValue>(ICollection<KeyValuePair<TKey, TValue?>> subject, string validator, IReadOnlyList<object?> arguments) {
        return validator switch {
            nameof(IsNotEmpty<int>) => new IsNotEmpty<KeyValuePair<TKey, TValue?>>(subject, _source, _validation),
            nameof(MinimumCountIs<int>) => new MinimumCountIs<KeyValuePair<TKey, TValue?>>(subject, GetCount(), _source, _validation),
            nameof(MaximumCountIs<int>) => new MaximumCountIs<KeyValuePair<TKey, TValue?>>(subject, GetCount(), _source, _validation),
            nameof(CountIs<int>) => new CountIs<KeyValuePair<TKey, TValue?>>(subject, GetCount(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };

        int GetCount() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<int>(validator, arguments, 0);
    }
}
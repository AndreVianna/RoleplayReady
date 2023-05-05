using System.Validation.Commands.Abstractions;

using ValidationResult = System.Results.ValidationResult;

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
        nameof(IsNullCommand) => new IsNullCommand(subject, _source, _validation),
        nameof(IsValidCommand) => new IsValidCommand((IValidatable)subject!, _source, _validation),
        _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
    };

    private IValidationCommand CreateNumberValidator<TValue>(TValue subject, string validator, IReadOnlyList<object?> arguments)
        where TValue : struct, IComparable<TValue> {
        return validator switch {
            nameof(IsLessThanCommand<TValue>) => new IsLessThanCommand<TValue>(subject, GetLimit(), _source, _validation),
            nameof(IsEqualToCommand<TValue>) => new IsEqualToCommand<TValue>(subject, GetLimit(), _source, _validation),
            nameof(IsGreaterThanCommand<TValue>) => new IsGreaterThanCommand<TValue>(subject, GetLimit(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };

        TValue GetLimit() => Ensure.ArgumentExistsAndIsOfType<TValue>(validator, arguments, 0);
    }

    private IValidationCommand CreateStringValidator(string subject, string? validator, IReadOnlyList<object?> arguments) {
        return validator switch {
            nameof(IsEmptyCommand) => new IsEmptyCommand(subject, _source, _validation),
            nameof(IsEmptyOrWhiteSpaceCommand) => new IsEmptyOrWhiteSpaceCommand(subject, _source, _validation),
            nameof(MinimumLengthIsCommand) => new MinimumLengthIsCommand(subject, GetLength(), _source, _validation),
            nameof(MaximumLengthIsCommand) => new MaximumLengthIsCommand(subject, GetLength(), _source, _validation),
            nameof(LengthIsCommand) => new LengthIsCommand(subject, GetLength(), _source, _validation),
            nameof(IsOneOfCommand<string>) => new IsOneOfCommand<string>(subject, GetList(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };

        int GetLength() => Ensure.ArgumentExistsAndIsOfType<int>(validator, arguments, 0);
        string?[] GetList() => Ensure.ArgumentsAreAllOfTypeOrDefault<string>(validator, arguments).ToArray();
    }

    private IValidationCommand CreateCollectionValidator<TItem>(ICollection<TItem?> subject, string validator, IReadOnlyList<object?> arguments) {
        return validator switch {
            nameof(IsEmpty<int>) => new IsEmpty<TItem>(subject, _source, _validation),
            nameof(MinimumCountIsCommand<int>) => new MinimumCountIsCommand<TItem>(subject, GetCount(), _source, _validation),
            nameof(MaximumCountIsCommand<int>) => new MaximumCountIsCommand<TItem>(subject, GetCount(), _source, _validation),
            nameof(CountIsCommand<int>) => new CountIsCommand<TItem>(subject, GetCount(), _source, _validation),
            nameof(Contains<int>) => new Contains<TItem>(subject, GetItem(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };

        int GetCount() => Ensure.ArgumentExistsAndIsOfType<int>(validator, arguments, 0);
        TItem? GetItem() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TItem>(validator, arguments, 0);
    }

    private IValidationCommand CreateDictionaryValidator<TKey, TValue>(IDictionary<TKey, TValue?> subject, string validator, IReadOnlyList<object?> arguments)
        where TKey : notnull {
        return validator switch {
            nameof(IsEmpty<int>) => new IsEmpty<KeyValuePair<TKey, TValue?>>(subject, _source, _validation),
            nameof(MinimumCountIsCommand<int>) => new MinimumCountIsCommand<KeyValuePair<TKey, TValue?>>(subject, GetCount(), _source, _validation),
            nameof(MaximumCountIsCommand<int>) => new MaximumCountIsCommand<KeyValuePair<TKey, TValue?>>(subject, GetCount(), _source, _validation),
            nameof(CountIsCommand<int>) => new CountIsCommand<KeyValuePair<TKey, TValue?>>(subject, GetCount(), _source, _validation),
            nameof(ContainsKeyCommand<int, int>) => new ContainsKeyCommand<TKey, TValue?>(subject, GetKey(), _source, _validation),
            nameof(ContainsValueCommand<int, int>) => new ContainsValueCommand<TKey, TValue?>(subject, GetValue(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };

        int GetCount() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<int>(validator, arguments, 0);
        TKey? GetKey() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TKey>(validator, arguments, 0);
        TValue? GetValue() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TValue>(validator, arguments, 0);
    }
}
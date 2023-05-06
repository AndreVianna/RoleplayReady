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

    public IValidationCommand Create(string command, params object?[] arguments)
        => _subject switch {
            null => new NullCommand(),
            IValidatable value => CreateObjectCommand(value, command),
            int value => CreateNumberCommand(value, command, arguments),
            decimal value => CreateNumberCommand(value, command, arguments),
            DateTime value => CreateDateTimeCommand(value, command, arguments),
            string value => CreateStringCommand(value, command, arguments),
            Type value => CreateTypeCommand(value, command, arguments),
            ICollection<int> value => CreateCollectionCommand(value, command, arguments),
            ICollection<decimal> value => CreateCollectionCommand(value, command, arguments),
            ICollection<int?> value => CreateCollectionCommand(value, command, arguments),
            ICollection<decimal?> value => CreateCollectionCommand(value, command, arguments),
            ICollection<string?> value => CreateCollectionCommand(value, command, arguments),
            IDictionary<string, int> value => CreateDictionaryCommand(value, command, arguments),
            IDictionary<string, decimal> value => CreateDictionaryCommand(value, command, arguments),
            IDictionary<string, int?> value => CreateDictionaryCommand(value, command, arguments),
            IDictionary<string, decimal?> value => CreateDictionaryCommand(value, command, arguments),
            IDictionary<string, string?> value => CreateDictionaryCommand(value, command, arguments),
            _ => throw new InvalidOperationException($"Unsupported data type '{typeof(TSubject).GetName()}'."),
        };

    private IValidationCommand CreateObjectCommand(object? subject, string command) => command switch {
        nameof(IsNullCommand) => new IsNullCommand(subject, _source, _validation),
        nameof(IsValidCommand) => new IsValidCommand((IValidatable)subject!, _source, _validation),
        _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
    };

    private IValidationCommand CreateNumberCommand<TValue>(TValue subject, string command, IReadOnlyList<object?> arguments)
        where TValue : struct, IComparable<TValue> {
        return command switch {
            nameof(IsLessThanCommand<TValue>) => new IsLessThanCommand<TValue>(subject, GetLimit(), _source, _validation),
            nameof(IsEqualToCommand<TValue>) => new IsEqualToCommand<TValue>(subject, GetLimit(), _source, _validation),
            nameof(IsGreaterThanCommand<TValue>) => new IsGreaterThanCommand<TValue>(subject, GetLimit(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
        };

        TValue GetLimit() => Ensure.ArgumentExistsAndIsOfType<TValue>(command, arguments, 0);
    }

    private IValidationCommand CreateDateTimeCommand(DateTime subject, string command, IReadOnlyList<object?> arguments) {
        return command switch {
            nameof(IsBeforeCommand) => new IsBeforeCommand(subject, GetLimit(), _source, _validation),
            nameof(IsAfterCommand) => new IsAfterCommand(subject, GetLimit(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
        };

        DateTime GetLimit() => Ensure.ArgumentExistsAndIsOfType<DateTime>(command, arguments, 0);
    }

    private IValidationCommand CreateTypeCommand(Type subject, string command, IReadOnlyList<object?> arguments) {
        return command switch {
            nameof(IsEqualToCommand<Type>) => new IsEqualToCommand<Type>(subject, GetTypeArg(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
        };

        Type GetTypeArg() => Ensure.ArgumentExistsAndIsOfType<Type>(command, arguments, 0);
    }

    private IValidationCommand CreateStringCommand(string subject, string command, IReadOnlyList<object?> arguments) {
        return command switch {
            nameof(IsEmptyCommand) => new IsEmptyCommand(subject, _source, _validation),
            nameof(IsEmptyOrWhiteSpaceCommand) => new IsEmptyOrWhiteSpaceCommand(subject, _source, _validation),
            nameof(MinimumLengthIsCommand) => new MinimumLengthIsCommand(subject, GetLength(), _source, _validation),
            nameof(MaximumLengthIsCommand) => new MaximumLengthIsCommand(subject, GetLength(), _source, _validation),
            nameof(LengthIsCommand) => new LengthIsCommand(subject, GetLength(), _source, _validation),
            nameof(IsOneOfCommand<string>) => new IsOneOfCommand<string>(subject, GetList(), _source, _validation),
            nameof(ContainsCommand) => new ContainsCommand(subject, GetSubString(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
        };

        int GetLength() => Ensure.ArgumentExistsAndIsOfType<int>(command, arguments, 0);
        string GetSubString() => Ensure.ArgumentExistsAndIsOfType<string>(command, arguments, 0);
        string?[] GetList() => Ensure.ArgumentsAreAllOfTypeOrDefault<string>(command, arguments).ToArray();
    }

    private IValidationCommand CreateCollectionCommand<TItem>(ICollection<TItem?> subject, string command, IReadOnlyList<object?> arguments) {
        return command switch {
            nameof(IsEmptyCommand) => new IsEmptyCommand<TItem>(subject, _source, _validation),
            nameof(MinimumCountIsCommand<int>) => new MinimumCountIsCommand<TItem>(subject, GetCount(), _source, _validation),
            nameof(MaximumCountIsCommand<int>) => new MaximumCountIsCommand<TItem>(subject, GetCount(), _source, _validation),
            nameof(CountIsCommand<int>) => new CountIsCommand<TItem>(subject, GetCount(), _source, _validation),
            nameof(ContainsCommand<int>) => new ContainsCommand<TItem>(subject, GetItem(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
        };

        int GetCount() => Ensure.ArgumentExistsAndIsOfType<int>(command, arguments, 0);
        TItem? GetItem() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TItem>(command, arguments, 0);
    }

    private IValidationCommand CreateDictionaryCommand<TKey, TValue>(IDictionary<TKey, TValue?> subject, string command, IReadOnlyList<object?> arguments)
        where TKey : notnull {
        return command switch {
            nameof(IsEmptyCommand) => new IsEmptyCommand<KeyValuePair<TKey, TValue?>>(subject, _source, _validation),
            nameof(MinimumCountIsCommand<int>) => new MinimumCountIsCommand<KeyValuePair<TKey, TValue?>>(subject, GetCount(), _source, _validation),
            nameof(MaximumCountIsCommand<int>) => new MaximumCountIsCommand<KeyValuePair<TKey, TValue?>>(subject, GetCount(), _source, _validation),
            nameof(CountIsCommand<int>) => new CountIsCommand<KeyValuePair<TKey, TValue?>>(subject, GetCount(), _source, _validation),
            nameof(ContainsKeyCommand<int, int>) => new ContainsKeyCommand<TKey, TValue?>(subject, GetKey(), _source, _validation),
            nameof(ContainsValueCommand<int, int>) => new ContainsValueCommand<TKey, TValue?>(subject, GetValue(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
        };

        int GetCount() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<int>(command, arguments, 0);
        TKey? GetKey() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TKey>(command, arguments, 0);
        TValue? GetValue() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TValue>(command, arguments, 0);
    }
}
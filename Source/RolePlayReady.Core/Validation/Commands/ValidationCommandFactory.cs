using static System.Constants.Constants.Commands;

using ValidationResult = System.Results.ValidationResult;

namespace System.Validation.Commands;

public sealed class ValidationCommandFactory {
    private readonly Type _subjectType;
    private readonly string _source;
    private readonly ValidationResult? _validation;

    private ValidationCommandFactory(Type subjectType, string source, ValidationResult? validation = null) {
        _subjectType = subjectType;
        _source = source;
        _validation = validation;
    }

    public static ValidationCommandFactory For(Type subjectType, string source, ValidationResult? validation = null)
        => new(subjectType, source, validation);

    public IValidationCommand Create(string command, params object?[] arguments) {
        if (command == IsNull) return new IsNullCommand(_source, _validation);
        if (command == IsEqualTo) return new IsEqualToCommand(arguments[0]!, _source, _validation);
        if (_subjectType == typeof(int)) return CreateNumberCommand<int>(command, arguments);
        if (_subjectType == typeof(decimal)) return CreateNumberCommand<decimal>(command, arguments);
        if (_subjectType == typeof(DateTime)) return CreateDateTimeCommand(command, arguments);
        if (_subjectType == typeof(int?)) return CreateNumberCommand<int>(command, arguments);
        if (_subjectType == typeof(decimal?)) return CreateNumberCommand<decimal>(command, arguments);
        if (_subjectType == typeof(DateTime?)) return CreateDateTimeCommand(command, arguments);
        if (_subjectType == typeof(string)) return CreateStringCommand(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(ICollection<int>))) return CreateCollectionCommand<int>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(ICollection<decimal>))) return CreateCollectionCommand<decimal>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(ICollection<int?>))) return CreateCollectionCommand<int>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(ICollection<decimal?>))) return CreateCollectionCommand<decimal>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(ICollection<string?>))) return CreateCollectionCommand<string>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(IDictionary<string, int>))) return CreateDictionaryCommand<string, int>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(IDictionary<string, decimal>))) return CreateDictionaryCommand<string, decimal>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(IDictionary<string, int?>))) return CreateDictionaryCommand<string, int>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(IDictionary<string, decimal?>))) return CreateDictionaryCommand<string, decimal>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(IDictionary<string, string>))) return CreateDictionaryCommand<string, string>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(IValidatable))) return CreateValidatableCommand(command);
        throw new InvalidOperationException($"Unsupported command '{command}' for type '{_subjectType.Name}'.");
    }

    private IValidationCommand CreateValidatableCommand(string command) {
        if (command == IsValid) return new IsValidCommand(_source, _validation);
        throw new InvalidOperationException($"Unsupported command '{command}' for {_subjectType.Name}.");
    }

    private IValidationCommand CreateNumberCommand<TValue>(string command, IReadOnlyList<object?> arguments)
        where TValue : struct, IComparable<TValue> {
        if (command == IsLessThan) return new IsLessThanCommand<TValue>(GetLimit(), _source, _validation);
        if (command == IsGreaterThan) return new IsGreaterThanCommand<TValue>(GetLimit(), _source, _validation);
        throw new InvalidOperationException($"Unsupported command '{command}' for type '{_subjectType.Name}'.");

        TValue GetLimit() => Ensure.ArgumentExistsAndIsOfType<TValue>(command, arguments, 0);
    }

    private IValidationCommand CreateDateTimeCommand(string command, IReadOnlyList<object?> arguments) {
        if (command == IsBefore) return new IsBeforeCommand(GetLimit(), _source, _validation);
        if (command == IsAfter) return new IsAfterCommand(GetLimit(), _source, _validation);
        throw new InvalidOperationException($"Unsupported command '{command}' for type '{_subjectType.Name}'.");

        DateTime GetLimit() => Ensure.ArgumentExistsAndIsOfType<DateTime>(command, arguments, 0);
    }

    private IValidationCommand CreateStringCommand(string command, IReadOnlyList<object?> arguments) {
        if (command == IsEmpty) return new IsEmptyCommand(_source, _validation);
        if (command == IsEmptyOrWhiteSpace) return new IsEmptyOrWhiteSpaceCommand(_source, _validation);
        if (command == LengthIsAtLeast) return new LengthIsAtLeastCommand(GetLength(), _source, _validation);
        if (command == LengthIsAtMost) return new LengthIsAtMostCommand(GetLength(), _source, _validation);
        if (command == LengthIs) return new LengthIsCommand(GetLength(), _source, _validation);
        if (command == Contains) return new ContainsCommand(GetString(), _source, _validation);
        if (command == IsIn) return new IsInCommand<string>(GetList(), _source, _validation);
        throw new InvalidOperationException($"Unsupported command '{command}' for type '{_subjectType.Name}'.");

        int GetLength() => Ensure.ArgumentExistsAndIsOfType<int>(command, arguments, 0);
        string GetString() => Ensure.ArgumentExistsAndIsOfType<string>(command, arguments, 0);
        string?[] GetList() => Ensure.ArgumentsAreAllOfTypeOrDefault<string>(command, arguments).ToArray();
    }

    private IValidationCommand CreateCollectionCommand<TItem>(string command, IReadOnlyList<object?> arguments) {
        if (command == IsEmpty) return new IsEmptyCommand<TItem>(_source, _validation);
        if (command == Contains) return new ContainsCommand<TItem>(GetItem(), _source, _validation);
        if (command == HasAtLeast) return new HasAtLeastCommand<TItem>(GetCount(), _source, _validation);
        if (command == HasAtMost) return new HasAtMostCommand<TItem>(GetCount(), _source, _validation);
        if (command == Has) return new HasCommand<TItem>(GetCount(), _source, _validation);
        throw new InvalidOperationException($"Unsupported command '{command}' for type '{_subjectType.Name}'.");

        int GetCount() => Ensure.ArgumentExistsAndIsOfType<int>(command, arguments, 0);
        TItem? GetItem() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TItem>(command, arguments, 0);
    }

    private IValidationCommand CreateDictionaryCommand<TKey, TValue>(string command, IReadOnlyList<object?> arguments)
        where TKey : notnull {
        if (command == IsEmpty) return new IsEmptyCommand<KeyValuePair<TKey, TValue?>>(_source, _validation);
        if (command == HasAtLeast) return new HasAtLeastCommand<KeyValuePair<TKey, TValue?>>(GetCount(), _source, _validation);
        if (command == HasAtMost) return new HasAtMostCommand<KeyValuePair<TKey, TValue?>>(GetCount(), _source, _validation);
        if (command == Has) return new HasCommand<KeyValuePair<TKey, TValue?>>(GetCount(), _source, _validation);
        if (command == ContainsKey) return new ContainsKeyCommand<TKey, TValue?>(GetKey(), _source, _validation);
        if (command == ContainsValue) return new ContainsValueCommand<TKey, TValue?>(GetValue(), _source, _validation);
        throw new InvalidOperationException($"Unsupported command '{command}' for type '{_subjectType.Name}'.");

        int GetCount() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<int>(command, arguments, 0);
        TKey? GetKey() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TKey>(command, arguments, 0);
        TValue? GetValue() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TValue>(command, arguments, 0);
    }
}
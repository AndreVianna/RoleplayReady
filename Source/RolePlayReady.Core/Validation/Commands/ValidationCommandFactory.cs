using static System.Constants.Constants.Commands;

namespace System.Validation.Commands;

public sealed class ValidationCommandFactory {
    private readonly Type _subjectType;
    private readonly string _source;

    private ValidationCommandFactory(Type subjectType, string source) {
        _subjectType = subjectType;
        _source = source;
    }

    public static ValidationCommandFactory For(Type subjectType, string source) => new(subjectType, source);

    public IValidationCommand Create(string command, params object?[] arguments) {
        if (command == IsNull)
            return new IsNullCommand(_source);
        if (command == IsEqualTo)
            return new IsEqualToCommand(arguments[0]!, _source);
        if (_subjectType == typeof(int))
            return CreateNumberCommand<int>(command, arguments);
        if (_subjectType == typeof(decimal))
            return CreateNumberCommand<decimal>(command, arguments);
        if (_subjectType == typeof(DateTime))
            return CreateDateTimeCommand(command, arguments);
        if (_subjectType == typeof(int?))
            return CreateNumberCommand<int>(command, arguments);
        if (_subjectType == typeof(decimal?))
            return CreateNumberCommand<decimal>(command, arguments);
        if (_subjectType == typeof(DateTime?))
            return CreateDateTimeCommand(command, arguments);
        if (_subjectType == typeof(string))
            return CreateStringCommand(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(ICollection<int>)))
            return CreateCollectionCommand<int>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(ICollection<decimal>)))
            return CreateCollectionCommand<decimal>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(ICollection<int?>)))
            return CreateCollectionCommand<int>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(ICollection<decimal?>)))
            return CreateCollectionCommand<decimal>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(ICollection<string?>)))
            return CreateCollectionCommand<string>(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(IDictionary<string, int>)))
            return CreateDictionaryCommand<string, int>(command, arguments);
        return _subjectType.IsAssignableTo(typeof(IDictionary<string, decimal>))
            ? CreateDictionaryCommand<string, decimal>(command, arguments)
            : _subjectType.IsAssignableTo(typeof(IDictionary<string, int?>))
            ? CreateDictionaryCommand<string, int>(command, arguments)
            : _subjectType.IsAssignableTo(typeof(IDictionary<string, decimal?>))
            ? CreateDictionaryCommand<string, decimal>(command, arguments)
            : _subjectType.IsAssignableTo(typeof(IDictionary<string, string>))
            ? CreateDictionaryCommand<string, string>(command, arguments)
            : _subjectType.IsAssignableTo(typeof(IValidatable))
            ? CreateValidatableCommand(command)
            : throw new InvalidOperationException($"Unsupported command '{command}' for type '{_subjectType.Name}'.");
    }

    private IValidationCommand CreateValidatableCommand(string command) => command == IsValid
            ? (IValidationCommand)new IsValidCommand(_source)
            : throw new InvalidOperationException($"Unsupported command '{command}' for {_subjectType.Name}.");

    private IValidationCommand CreateNumberCommand<TValue>(string command, IReadOnlyList<object?> arguments)
        where TValue : struct, IComparable<TValue> {
        return command == IsLessThan
            ? new IsLessThanCommand<TValue>(GetLimit(), _source)
            : command == IsGreaterThan
            ? (IValidationCommand)new IsGreaterThanCommand<TValue>(GetLimit(), _source)
            : throw new InvalidOperationException($"Unsupported command '{command}' for type '{_subjectType.Name}'.");

        TValue GetLimit() => Ensure.ArgumentExistsAndIsOfType<TValue>(command, arguments, 0);
    }

    private IValidationCommand CreateDateTimeCommand(string command, IReadOnlyList<object?> arguments) {
        return command == IsBefore
            ? new IsBeforeCommand(GetLimit(), _source)
            : command == IsAfter
            ? (IValidationCommand)new IsAfterCommand(GetLimit(), _source)
            : throw new InvalidOperationException($"Unsupported command '{command}' for type '{_subjectType.Name}'.");

        DateTime GetLimit() => Ensure.ArgumentExistsAndIsOfType<DateTime>(command, arguments, 0);
    }

    private IValidationCommand CreateStringCommand(string command, IReadOnlyList<object?> arguments) {
        if (command == IsEmpty)
            return new IsEmptyCommand(_source);
        if (command == IsEmptyOrWhiteSpace)
            return new IsEmptyOrWhiteSpaceCommand(_source);
        if (command == LengthIsAtLeast)
            return new LengthIsAtLeastCommand(GetLength(), _source);
        if (command == LengthIsAtMost)
            return new LengthIsAtMostCommand(GetLength(), _source);
        return command == LengthIs
            ? new LengthIsCommand(GetLength(), _source)
            : command == Contains
            ? new ContainsCommand(GetString(), _source)
            : command == IsIn
            ? new IsInCommand<string>(GetList(), _source)
            : command == IsEmail
            ? new IsEmailCommand(_source)
            : command == IsPassword
            ? (IValidationCommand)new IsPasswordCommand(GetPolicy(), _source)
            : throw new InvalidOperationException($"Unsupported command '{command}' for type '{_subjectType.Name}'.");

        int GetLength() => Ensure.ArgumentExistsAndIsOfType<int>(command, arguments, 0);
        string GetString() => Ensure.ArgumentExistsAndIsOfType<string>(command, arguments, 0);
        string?[] GetList() => [.. Ensure.ArgumentsAreAllOfTypeOrDefault<string>(command, arguments)];
        IPasswordPolicy GetPolicy() => Ensure.ArgumentExistsAndIsOfType<IPasswordPolicy>(command, arguments, 0);
    }

    private IValidationCommand CreateCollectionCommand<TItem>(string command, IReadOnlyList<object?> arguments) {
        return command == IsEmpty
            ? new IsEmptyCommand<TItem>(_source)
            : command == Contains
            ? new ContainsCommand<TItem>(GetItem(), _source)
            : command == HasAtLeast
            ? new HasAtLeastCommand<TItem>(GetCount(), _source)
            : command == HasAtMost
            ? new HasAtMostCommand<TItem>(GetCount(), _source)
            : command == Has
            ? (IValidationCommand)new HasCommand<TItem>(GetCount(), _source)
            : throw new InvalidOperationException($"Unsupported command '{command}' for type '{_subjectType.Name}'.");

        int GetCount() => Ensure.ArgumentExistsAndIsOfType<int>(command, arguments, 0);
        TItem? GetItem() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TItem>(command, arguments, 0);
    }

    private IValidationCommand CreateDictionaryCommand<TKey, TValue>(string command, IReadOnlyList<object?> arguments)
        where TKey : notnull {
        if (command == IsEmpty)
            return new IsEmptyCommand<KeyValuePair<TKey, TValue?>>(_source);
        return command == HasAtLeast
            ? new HasAtLeastCommand<KeyValuePair<TKey, TValue?>>(GetCount(), _source)
            : command == HasAtMost
            ? new HasAtMostCommand<KeyValuePair<TKey, TValue?>>(GetCount(), _source)
            : command == Has
            ? new HasCommand<KeyValuePair<TKey, TValue?>>(GetCount(), _source)
            : command == ContainsKey
            ? new ContainsKeyCommand<TKey, TValue?>(GetKey(), _source)
            : command == ContainsValue
            ? (IValidationCommand)new ContainsValueCommand<TKey, TValue?>(GetValue(), _source)
            : throw new InvalidOperationException($"Unsupported command '{command}' for type '{_subjectType.Name}'.");

        int GetCount() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<int>(command, arguments, 0);
        TKey GetKey() => Ensure.ArgumentExistsAndIsOfType<TKey>(command, arguments, 0);
        TValue? GetValue() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TValue>(command, arguments, 0);
    }
}
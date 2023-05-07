using CommandsNames = System.Constants.Constants.Commands;
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
        if (_subjectType == typeof(int)) return CreateNumberCommand<int>(command, arguments);
        if (_subjectType == typeof(decimal)) return CreateNumberCommand<decimal>(command, arguments);
        if (_subjectType == typeof(DateTime)) return CreateDateTimeCommand(command, arguments);
        if (_subjectType == typeof(int?)) return CreateNumberCommand<int>(command, arguments);
        if (_subjectType == typeof(decimal?)) return CreateNumberCommand<decimal>(command, arguments);
        if (_subjectType == typeof(DateTime?)) return CreateDateTimeCommand(command, arguments);
        if (_subjectType == typeof(string)) return CreateStringCommand(command, arguments);
        if (_subjectType.IsAssignableTo(typeof(Type))) return CreateTypeCommand(command, arguments);
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
        return CreateObjectCommand(command);
    }

    private IValidationCommand CreateValidatableCommand(string command)
        => command switch {
            CommandsNames.IsNullCommand => new IsNullCommand(_source, _validation),
            CommandsNames.IsValidCommand => new IsValidCommand(_source, _validation),
            _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
        };

    private IValidationCommand CreateObjectCommand(string command)
        => command switch {
            CommandsNames.IsNullCommand => new IsNullCommand(_source, _validation),
            _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
        };

    private IValidationCommand CreateNumberCommand<TValue>(string command, IReadOnlyList<object?> arguments)
        where TValue : struct, IComparable<TValue> {
        return command switch {
            _ when command == CommandsNames.IsEqualToCommand => new IsEqualToCommand<TValue>(GetLimit(), _source, _validation),
            _ when command == CommandsNames.IsLessThanCommand => new IsLessThanCommand<TValue>(GetLimit(), _source, _validation),
            _ when command == CommandsNames.IsGreaterThanCommand => new IsGreaterThanCommand<TValue>(GetLimit(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
        };

        TValue GetLimit() => Ensure.ArgumentExistsAndIsOfType<TValue>(command, arguments, 0);
    }

    private IValidationCommand CreateDateTimeCommand(string command, IReadOnlyList<object?> arguments) {
        return command switch {
            _ when command == CommandsNames.IsEqualToCommand => new IsEqualToCommand<DateTime>(GetLimit(), _source, _validation),
            CommandsNames.IsBeforeCommand => new IsBeforeCommand(GetLimit(), _source, _validation),
            CommandsNames.IsAfterCommand => new IsAfterCommand(GetLimit(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
        };

        DateTime GetLimit() => Ensure.ArgumentExistsAndIsOfType<DateTime>(command, arguments, 0);
    }

    private IValidationCommand CreateTypeCommand(string command, IReadOnlyList<object?> arguments) {
        return command switch {
            _ when command == CommandsNames.IsEqualToCommand => new IsEqualToCommand<Type>(GetTypeArg(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
        };

        Type GetTypeArg() => Ensure.ArgumentExistsAndIsOfType<Type>(command, arguments, 0);
    }

    private IValidationCommand CreateStringCommand(string command, IReadOnlyList<object?> arguments) {
        return command switch {
            _ when command == CommandsNames.IsEqualToCommand => new IsEqualToCommand<string>(GetString(), _source, _validation),
            CommandsNames.IsEmptyCommand => new IsEmptyCommand(_source, _validation),
            CommandsNames.IsEmptyOrWhiteSpaceCommand => new IsEmptyOrWhiteSpaceCommand(_source, _validation),
            CommandsNames.MinimumLengthIsCommand => new MinimumLengthIsCommand(GetLength(), _source, _validation),
            CommandsNames.MaximumLengthIsCommand => new MaximumLengthIsCommand(GetLength(), _source, _validation),
            CommandsNames.LengthIsCommand => new LengthIsCommand(GetLength(), _source, _validation),
            CommandsNames.ContainsCommand => new ContainsCommand(GetString(), _source, _validation),
            _ when command == CommandsNames.IsOneOfCommand => new IsOneOfCommand<string>(GetList(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
        };

        int GetLength() => Ensure.ArgumentExistsAndIsOfType<int>(command, arguments, 0);
        string GetString() => Ensure.ArgumentExistsAndIsOfType<string>(command, arguments, 0);
        string?[] GetList() => Ensure.ArgumentsAreAllOfTypeOrDefault<string>(command, arguments).ToArray();
    }

    private IValidationCommand CreateCollectionCommand<TItem>(string command, IReadOnlyList<object?> arguments) {
        return command switch {
            CommandsNames.IsEmptyCommand => new IsEmptyCommand<TItem>(_source, _validation),
            CommandsNames.ContainsCommand => new ContainsCommand<TItem>(GetItem(), _source, _validation),
            _ when command == CommandsNames.MinimumCountIsCommand => new MinimumCountIsCommand<TItem>(GetCount(), _source, _validation),
            _ when command == CommandsNames.MaximumCountIsCommand => new MaximumCountIsCommand<TItem>(GetCount(), _source, _validation),
            _ when command == CommandsNames.CountIsCommand => new CountIsCommand<TItem>(GetCount(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
        };

        int GetCount() => Ensure.ArgumentExistsAndIsOfType<int>(command, arguments, 0);
        TItem? GetItem() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TItem>(command, arguments, 0);
    }

    private IValidationCommand CreateDictionaryCommand<TKey, TValue>(string command, IReadOnlyList<object?> arguments)
        where TKey : notnull {
        return command switch {
            CommandsNames.IsEmptyCommand => new IsEmptyCommand<KeyValuePair<TKey, TValue?>>(_source, _validation),
            _ when command == CommandsNames.MinimumCountIsCommand => new MinimumCountIsCommand<KeyValuePair<TKey, TValue?>>(GetCount(), _source, _validation),
            _ when command == CommandsNames.MaximumCountIsCommand => new MaximumCountIsCommand<KeyValuePair<TKey, TValue?>>(GetCount(), _source, _validation),
            _ when command == CommandsNames.CountIsCommand => new CountIsCommand<KeyValuePair<TKey, TValue?>>(GetCount(), _source, _validation),
            _ when command == CommandsNames.ContainsKeyCommand => new ContainsKeyCommand<TKey, TValue?>(GetKey(), _source, _validation),
            _ when command == CommandsNames.ContainsValueCommand => new ContainsValueCommand<TKey, TValue?>(GetValue(), _source, _validation),
            _ => throw new InvalidOperationException($"Unsupported command '{command}'.")
        };

        int GetCount() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<int>(command, arguments, 0);
        TKey? GetKey() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TKey>(command, arguments, 0);
        TValue? GetValue() => Ensure.ArgumentExistsAndIsOfTypeOrDefault<TValue>(command, arguments, 0);
    }
}
using static System.StringSplitOptions;

using MaximumLengthIs = System.Validators.Text.MaximumLengthIs;
using MinimumLengthIs = System.Validators.Text.MinimumLengthIs;

namespace System.Validators;

public sealed class ValidatorFactory {
    private readonly string _source;

    private ValidatorFactory(string source) {
        _source = source;
    }

    public static ValidatorFactory For(string source) => new(source);

    public IValidator Create(Type dataType, string validator, object[] arguments) {
        return GetTypeComponents() switch {
            ["Integer"] => CreateNumericValidator<int>(validator, arguments),
            ["Decimal"] => CreateNumericValidator<decimal>(validator, arguments),
            ["String"] => CreateStringValidator(validator, arguments),
            ["List", "Integer"] => CreateCollectionValidator<int>(validator, arguments),
            ["List", "String"] => CreateCollectionValidator<string>(validator, arguments),
            ["Dictionary", "String", "Integer"] => CreateDictionaryValidator<string, int>(validator, arguments),
            ["Dictionary", "String", "Decimal"] => CreateDictionaryValidator<string, decimal>(validator, arguments),
            ["Dictionary", "String", "String"] => CreateDictionaryValidator<string, string>(validator, arguments),
            _ => throw new InvalidOperationException($"Unsupported validator data type '{dataType.GetName()}'."),
        };

        string[] GetTypeComponents()
            => dataType
              .GetName()
              .Split(new[] { '<', ',', ' ', '>' }, RemoveEmptyEntries)
              .ToArray();
    }

    private IValidator CreateNumericValidator<TValue>(string validator, IReadOnlyList<object> arguments)
        where TValue : IComparable<TValue> {
        return validator switch {
            nameof(IsLessThan<TValue>) => new IsLessThan<TValue>(_source, GetLimit()),
            nameof(MinimumIs<TValue>) => new MinimumIs<TValue>(_source, GetLimit()),
            nameof(IsEqualTo<TValue>) => new IsEqualTo<TValue>(_source, GetLimit()),
            nameof(MaximumIs<TValue>) => new MaximumIs<TValue>(_source, GetLimit()),
            nameof(IsGreaterThan<TValue>) => new IsGreaterThan<TValue>(_source, GetLimit()),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };

        TValue GetLimit() => Ensure.ArgumentExistsAndIsOfType<TValue>(arguments, validator, 0);
    }

    private IValidator CreateStringValidator(string validator, IReadOnlyList<object> arguments) {
        return validator switch {
            nameof(MinimumLengthIs) => new MinimumLengthIs(_source, GetLength()),
            nameof(MaximumLengthIs) => new MaximumLengthIs(_source, GetLength()),
            nameof(LengthIs) => new LengthIs(_source, GetLength()),
            nameof(IsOneOf) => new IsOneOf(_source, GetList()),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };

        int GetLength() => Ensure.ArgumentExistsAndIsOfType<int>(arguments, validator, 0);
        string[] GetList() => Ensure.ArgumentsAreAllOfType<string>(arguments, validator).ToArray();
    }

    private IValidator CreateCollectionValidator<TItem>(string validator, IReadOnlyList<object> arguments) {
        return validator switch {
            nameof(MinimumCountIs<TItem>) => new MinimumCountIs<TItem>(_source, GetCount()),
            nameof(MaximumCountIs<TItem>) => new MaximumCountIs<TItem>(_source, GetCount()),
            nameof(CountIs<TItem>) => new CountIs<TItem>(_source, GetCount()),
            nameof(Contains<TItem>) => new Contains<TItem>(_source, GetItem()),
            nameof(NotContains<TItem>) => new NotContains<TItem>(_source, GetItem()),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };

        int GetCount() => Ensure.ArgumentExistsAndIsOfType<int>(arguments, validator, 0);
        TItem GetItem() => Ensure.ArgumentExistsAndIsOfType<TItem>(arguments, validator, 0);
    }

    private IValidator CreateDictionaryValidator<TKey, TValue>(string validator, IReadOnlyList<object> arguments) {
        var count = Ensure.ArgumentExistsAndIsOfType<int>(arguments, validator, 0);
        return validator switch {
            nameof(MinimumCountIs<KeyValuePair<TKey, TValue>>) => new MinimumCountIs<KeyValuePair<TKey, TValue>>(_source, count),
            nameof(MaximumCountIs<KeyValuePair<TKey, TValue>>) => new MaximumCountIs<KeyValuePair<TKey, TValue>>(_source, count),
            nameof(CountIs<KeyValuePair<TKey, TValue>>) => new CountIs<KeyValuePair<TKey, TValue>>(_source, count),
            //nameof(Contains<TItem>) => new Contains<TItem>(_source, arguments.OfType<TItem>()),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };
    }
}
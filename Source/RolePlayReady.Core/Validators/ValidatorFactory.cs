using static System.StringSplitOptions;

using MaximumLengthIs = System.Validators.Text.MaximumLengthIs;
using MinimumLengthIs = System.Validators.Text.MinimumLengthIs;

namespace System.Validators;

public sealed partial class ValidatorFactory {
    private readonly string _source;

    private ValidatorFactory(string source) {
        _source = source;
    }

    public static ValidatorFactory For(string source) => new(source);

    public IValidator Create(Type dataType, string validator, object?[] arguents) {
        return GetTypeComponents() switch {
            ["Integer"] => CreateNumericValidator<int>(validator, arguents),
            ["Decimal"] => CreateNumericValidator<decimal>(validator, arguents),
            ["String"] => CreateStringValidator(validator, arguents),
            ["List", "Integer"] => CreateCollectionValidator<int>(validator, arguents),
            ["List", "String"] => CreateCollectionValidator<string>(validator, arguents),
            ["Dictionary", "String", "Integer"] => CreateDictionaryValidator<string, int>(validator, arguents),
            ["Dictionary", "String", "Decimal"] => CreateDictionaryValidator<string, decimal>(validator, arguents),
            ["Dictionary", "String", "String"] => CreateDictionaryValidator<string, string>(validator, arguents),
            _ => throw new InvalidOperationException($"Unsupported validator data type '{dataType.GetFriendlyName()}'."),
        };

        string[] GetTypeComponents()
            => dataType
              .GetFriendlyName()
              .Split(new[] { '<', ',', ' ', '>' }, RemoveEmptyEntries)
              .ToArray();
    }

    private IValidator CreateNumericValidator<TValue>(string validator, IReadOnlyList<object?> arguments)
        where TValue : IComparable<TValue> {
        var limit = Ensure.ArgumentExistsAndIsOfType<TValue>(validator, 0, arguments);
        return validator switch {
            nameof(IsLessThan<TValue>) => new IsLessThan<TValue>(_source, limit),
            nameof(MinimumIs<TValue>) => new MinimumIs<TValue>(_source, limit),
            nameof(IsEqualTo<TValue>) => new IsEqualTo<TValue>(_source, limit),
            nameof(MaximumIs<TValue>) => new MaximumIs<TValue>(_source, limit),
            nameof(IsGreaterThan<TValue>) => new IsGreaterThan<TValue>(_source, limit),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };
    }

    private IValidator CreateStringValidator(string validator, IReadOnlyList<object?> arguments) {
        var length = Ensure.ArgumentExistsAndIsOfType<int>(validator, 0, arguments);
        return validator switch {
            nameof(MinimumLengthIs) => new MinimumLengthIs(_source, length),
            nameof(MaximumLengthIs) => new MaximumLengthIs(_source, length),
            nameof(LengthIs) => new LengthIs(_source, length),
            //nameof(OneOf) => new OneOf(_source, arguments.OfType<string>()),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };
    }

    private IValidator CreateCollectionValidator<TItem>(string validator, IReadOnlyList<object?> arguments) {
        var count = Ensure.ArgumentExistsAndIsOfType<int>(validator, 0, arguments);
        return validator switch {
            nameof(MinimumCountIs<TItem>) => new MinimumCountIs<TItem>(_source, count),
            nameof(MaximumCountIs<TItem>) => new MaximumCountIs<TItem>(_source, count),
            nameof(CountIs<TItem>) => new CountIs<TItem>(_source, count),
            //nameof(Contains<TItem>) => new Contains<TItem>(_source, arguments.OfType<TItem>()),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };
    }

    private IValidator CreateDictionaryValidator<TKey, TValue>(string validator, IReadOnlyList<object?> arguments) {
        var count = Ensure.ArgumentExistsAndIsOfType<int>(validator, 0, arguments);
        return validator switch {
            nameof(MinimumCountIs<KeyValuePair<TKey, TValue>>) => new MinimumCountIs<KeyValuePair<TKey, TValue>>(_source, count),
            nameof(MaximumCountIs<KeyValuePair<TKey, TValue>>) => new MaximumCountIs<KeyValuePair<TKey, TValue>>(_source, count),
            nameof(CountIs<KeyValuePair<TKey, TValue>>) => new CountIs<KeyValuePair<TKey, TValue>>(_source, count),
            //nameof(Contains<TItem>) => new Contains<TItem>(_source, arguments.OfType<TItem>()),
            _ => throw new InvalidOperationException($"Unsupported validator '{validator}'.")
        };
    }

    [GeneratedRegex("([a-z]+)", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase)]
    private static partial Regex GetTypes();
}
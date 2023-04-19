using static System.StringSplitOptions;

using MaximumLengthIs = System.Validators.Text.MaximumLengthIs;
using MinimumLengthIs = System.Validators.Text.MinimumLengthIs;

namespace System.Validators;

public partial class ValidatorFactory {
    private readonly string _source;

    private ValidatorFactory(string source) {
        _source = source;
    }

    public static ValidatorFactory For(string source) => new(source);

    public IValidator Create(Type valueType, string validator, object?[] args) {
        return GetTypeComponents() switch {
            ["Integer"] => CreateNumericValidator<int>(validator, args),
            ["Decimal"] => CreateNumericValidator<decimal>(validator, args),
            ["String"] => CreateStringValidator(validator, args),
            ["List", "Integer"] => CreateCollectionValidator<int>(validator, args),
            ["List", "String"] => CreateCollectionValidator<string>(validator, args),
            ["Dictionary", "String", "Integer"] => CreateDictionaryValidator<string, int>(validator, args),
            ["Dictionary", "String", "Decimal"] => CreateDictionaryValidator<string, decimal>(validator, args),
            ["Dictionary", "String", "String"] => CreateDictionaryValidator<string, string>(validator, args),
            _ => throw new ArgumentException($"Unsupported data type: {valueType.GetFriendlyName()}"),
        };

        string[] GetTypeComponents()
            => valueType
              .GetFriendlyName()
              .Split(new[] { '<', ',', ' ', '>' }, RemoveEmptyEntries)
              .ToArray();
    }

    private IValidator CreateNumericValidator<TValue>(string validator, IReadOnlyList<object?> args)
        where TValue : IComparable<TValue> {
        var limit = Ensure.ArgumentExistsAndIsOfType<TValue>(validator, 0, args);
        return validator switch {
            nameof(LessThan<TValue>) => new LessThan<TValue>(_source, limit),
            nameof(MinimumValueIs<TValue>) => new MinimumValueIs<TValue>(_source, limit),
            nameof(EqualTo<TValue>) => new EqualTo<TValue>(_source, limit),
            nameof(MaximumValueIs<TValue>) => new MaximumValueIs<TValue>(_source, limit),
            nameof(GreaterThan<TValue>) => new GreaterThan<TValue>(_source, limit),
            _ => throw new ArgumentException($"Unsupported validator: {validator}.")
        };
    }

    private IValidator CreateStringValidator(string validator, IReadOnlyList<object?> args) {
        var length = Ensure.ArgumentExistsAndIsOfType<int>(validator, 0, args);
        return validator switch {
            nameof(MinimumLengthIs) => new MinimumLengthIs(_source, length),
            nameof(MaximumLengthIs) => new MaximumLengthIs(_source, length),
            nameof(LengthIs) => new LengthIs(_source, length),
            //nameof(OneOf) => new OneOf(_source, args.OfType<string>()),
            _ => throw new ArgumentException($"Unsupported validator: {validator}.")
        };
    }

    private IValidator CreateCollectionValidator<TItem>(string validator, IReadOnlyList<object?> args) {
        var count = Ensure.ArgumentExistsAndIsOfType<int>(validator, 0, args);
        return validator switch {
            nameof(MinimumCountIs<TItem>) => new MinimumCountIs<TItem>(_source, count),
            nameof(MaximumCountIs<TItem>) => new MaximumCountIs<TItem>(_source, count),
            nameof(CountIs<TItem>) => new CountIs<TItem>(_source, count),
            //nameof(Contains<TItem>) => new Contains<TItem>(_source, args.OfType<TItem>()),
            _ => throw new ArgumentException($"Unsupported validator: {validator}.")
        };
    }

    private IValidator CreateDictionaryValidator<TKey, TValue>(string validator, IReadOnlyList<object?> args) {
        var count = Ensure.ArgumentExistsAndIsOfType<int>(validator, 0, args);
        return validator switch {
            nameof(MinimumCountIs<KeyValuePair<TKey, TValue>>) => new MinimumCountIs<KeyValuePair<TKey, TValue>>(_source, count),
            nameof(MaximumCountIs<KeyValuePair<TKey, TValue>>) => new MaximumCountIs<KeyValuePair<TKey, TValue>>(_source, count),
            nameof(CountIs<KeyValuePair<TKey, TValue>>) => new CountIs<KeyValuePair<TKey, TValue>>(_source, count),
            //nameof(Contains<TItem>) => new Contains<TItem>(_source, args.OfType<TItem>()),
            _ => throw new ArgumentException($"Unsupported validator: {validator}.")
        };
    }

    [GeneratedRegex("([a-z]+)", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase)]
    private static partial Regex GetTypes();
}
using System.Validators.Collection;

namespace System.Validators;

public partial class ValidatorFactory {
    private readonly string _source;

    private ValidatorFactory(string source) {
        _source = source;
    }

    public static ValidatorFactory For(string source) => new(source);

    public IValidator Create(string typeDescription, string validator, params object?[] args) {
        var types = typeDescription.Split(new []{ '<', ',', ' ', '>' }, StringSplitOptions.RemoveEmptyEntries);
        return types switch {
            ["int"] => CreateNumericValidator<int>(validator, args),
            ["decimal"] => CreateNumericValidator<decimal>(validator, args),
            ["sting"] => CreateStringValidator(validator, args),
            ["list", "int"] => CreateCollectionValidator<int>(validator, args),
            ["list", "string"] => CreateCollectionValidator<string>(validator, args),
            ["map", "string", "int"] => CreateDictionaryValidator<string, int>(validator, args),
            ["map", "string", "decimal"] => CreateDictionaryValidator<string, decimal>(validator, args),
            ["map", "string", "string"] => CreateDictionaryValidator<string, string>(validator, args),
            _ => throw new ArgumentException($"Unsupported data type: {typeDescription}"),
        };
    }

    private IValidator CreateNumericValidator<TValue>(string validator, IReadOnlyList<object?> args)
        where TValue : IComparable<TValue> {
        if (args.Count == 0 || args[0] is not TValue threshold) throw new InvalidOperationException();
        return validator switch {
            nameof(MaximumValueExclusive<TValue>) => new MaximumValueExclusive<TValue>(_source, threshold),
            nameof(MaximumValue<TValue>) => new MaximumValue<TValue>(_source, threshold),
            nameof(MinimumValueExclusive<TValue>) => new MinimumValueExclusive<TValue>(_source, threshold),
            nameof(MinimumValue<TValue>) => new MinimumValue<TValue>(_source, threshold),
            _ => throw new ArgumentException($"Invalid validation type for a number: {validator}({string.Join(", ", args.Select(i => i is string ? $"'{i}'" : $"{i}").ToArray())}).")
        };
    }

    private IValidator CreateStringValidator(string validator, IReadOnlyList<object?> args) {
        if (args.Count == 0) throw new InvalidOperationException();
        return validator switch {
            nameof(MinimumLength) when args[0] is int length => new MinimumLength(_source, length),
            nameof(MaximumLength) when args[0] is int length => new MaximumLength(_source, length),
            nameof(ExactLength) when args[0] is int length => new ExactLength(_source, length),
            //nameof(OneOf) => new OneOf(_source, args.OfType<string>()),
            _ => throw new ArgumentException($"Invalid validation type for a text: {validator}({string.Join(", ", args.Select(i => i is string ? $"'{i}'" : $"{i}").ToArray())}).")
        };
    }

    private IValidator CreateCollectionValidator<TItem>(string validator, IReadOnlyList<object?> args) {
        if (args.Count == 0) throw new InvalidOperationException();
        return validator switch {
            nameof(MinimumCount<TItem>) when args[0] is int size => new MinimumCount<TItem>(_source, size),
            nameof(MaximumCount<TItem>) when args[0] is int size => new MaximumCount<TItem>(_source, size),
            nameof(ExactCount<TItem>) when args[0] is int size => new ExactCount<TItem>(_source, size),
            //nameof(Contains<TItem>) => new Contains<TItem>(_source, args.OfType<TItem>()),
            _ => throw new ArgumentException($"Invalid validation type for a text: {validator}({string.Join(", ", args.Select(i => i is string ? $"'{i}'" : $"{i}").ToArray())}).")
        };
    }

    private IValidator CreateDictionaryValidator<TKey, TValue>(string validator, IReadOnlyList<object?> args) {
        if (args.Count == 0) throw new InvalidOperationException();
        return validator switch {
            nameof(MinimumCount<KeyValuePair<TKey, TValue>>) when args[0] is int size => new MinimumCount<KeyValuePair<TKey, TValue>>(_source, size),
            nameof(MaximumCount<KeyValuePair<TKey, TValue>>) when args[0] is int size => new MaximumCount<KeyValuePair<TKey, TValue>>(_source, size),
            nameof(ExactCount<KeyValuePair<TKey, TValue>>) when args[0] is int size => new ExactCount<KeyValuePair<TKey, TValue>>(_source, size),
            //nameof(Contains<TItem>) => new Contains<TItem>(_source, args.OfType<TItem>()),
            _ => throw new ArgumentException($"Invalid validation type for a text: {validator}({string.Join(", ", args.Select(i => i is string ? $"'{i}'" : $"{i}").ToArray())}).")
        };
    }

    [GeneratedRegex("([a-z]+)", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase)]
    private static partial Regex GetTypes();
}
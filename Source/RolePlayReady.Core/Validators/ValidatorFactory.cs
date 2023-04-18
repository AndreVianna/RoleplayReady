namespace System.Validators;

public partial class ValidatorFactory {
    private readonly string _source;

    private ValidatorFactory(string source) {
        _source = source;
    }

    public static ValidatorFactory For(string source) => new(source);

    private static readonly Regex _genericTypeEnd = GenericPostfix();
    public IValidator Create<TValue>(string validatorType, params object?[] args)
        => _genericTypeEnd.Replace(typeof(TValue).Name, string.Empty) switch {
            nameof(Int32) => CreateNumericValidator<int>(validatorType, args),
            nameof(Decimal) => CreateNumericValidator<decimal>(validatorType, args),
            nameof(String) => CreateStringValidator(validatorType, args),
            nameof(ICollection) => CreateCollectionValidator(validatorType, args),
            _ => throw new ArgumentException($"Unsupported data type: {typeof(TValue).Name}"),
        };

    private IValidator CreateNumericValidator<TValue>(string validatorType, IReadOnlyList<object?> args)
        where TValue : IComparable<TValue>
        => $"{validatorType}Validator" switch {
            nameof(LessThanValidator<TValue>) => args.Count == 0 || args[0] is not TValue maximum
                ? throw new InvalidOperationException()
                : new LessThanValidator<TValue>(_source, maximum),
            nameof(GreaterThanValidator<TValue>) => args.Count == 0 || args[0] is not TValue minimum
                ? throw new InvalidOperationException()
                : new GreaterThanValidator<TValue>(_source, minimum),
            _ => throw new ArgumentException($"Invalid validation type for numeric data: {validatorType}")
        };

    private static IValidator CreateStringValidator(string validatorType, object?[] args)
        => throw new NotImplementedException();

    private static IValidator CreateCollectionValidator(string validatorType, object?[] args)
        => throw new NotImplementedException();

    [GeneratedRegex("`.*$", RegexOptions.Compiled)]
    private static partial Regex GenericPostfix();
}
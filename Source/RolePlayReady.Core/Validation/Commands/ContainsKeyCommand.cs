namespace System.Validation.Commands;

public sealed class ContainsKeyCommand<TKey, TValue>
    : ValidationCommand
    where TKey : notnull {
    public ContainsKeyCommand(TKey? key, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = d => key is not null && ((IDictionary<TKey, TValue?>)d!).ContainsKey(key);
        NegateAs = d => key is not null && !((IDictionary<TKey, TValue?>)d!).ContainsKey(key);
        ValidationErrorMessage = MustContainKey;
        GetArguments = _ => new object?[] { key };
    }
}
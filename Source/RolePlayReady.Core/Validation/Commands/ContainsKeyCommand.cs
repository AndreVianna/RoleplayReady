namespace System.Validation.Commands;

public sealed class ContainsKeyCommand<TKey, TValue>
    : ValidationCommand
    where TKey : notnull {
    public ContainsKeyCommand(TKey? key, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = d => ((IDictionary<TKey, TValue?>)d!).ContainsKey(key);
        ValidationErrorMessage = MustContainKey;
        GetErrorMessageArguments = _ => new object?[] { key };
    }
}
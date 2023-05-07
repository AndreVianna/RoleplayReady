namespace System.Validation.Commands;

public sealed class ContainsKeyCommand<TKey, TValue>
    : ValidationCommand<IDictionary<TKey, TValue?>>
    where TKey : notnull {
    public ContainsKeyCommand(TKey? key, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => key is not null && s.ContainsKey(key);
        NegateAs = s => key is not null && !s.ContainsKey(key);
        ValidationErrorMessage = MustContainKey;
        Arguments = SetArguments(key);
    }
}
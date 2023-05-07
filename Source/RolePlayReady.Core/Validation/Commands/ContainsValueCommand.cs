namespace System.Validation.Commands;

public sealed class ContainsValueCommand<TKey, TValue>
    : ValidationCommand<IDictionary<TKey, TValue?>>
    where TKey : notnull {
    public ContainsValueCommand(TValue? value, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s.Values.Contains(value);
        ValidationErrorMessage = MustContainValue;
        Arguments = SetArguments(value);
    }
}
namespace System.Validation.Commands;

public sealed class ContainsValueCommand<TKey, TValue>
    : ValidationCommand<IDictionary<TKey, TValue?>>
    where TKey : notnull {

    public ContainsValueCommand(IDictionary<TKey, TValue?> subject, TValue? value, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.Values.Contains(value);
        ValidationErrorMessage = MustContainValue;
        Arguments = SetArguments(value);
    }
}
namespace System.Validation.Commands;

public sealed class ContainsValueCommand<TKey, TValue>
    : ValidationCommand
    where TKey : notnull {
    public ContainsValueCommand(TValue? value, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = d => ((IDictionary<TKey, TValue?>)d!).Values.Contains(value);
        ValidationErrorMessage = MustContainValue;
        GetArguments = _ => new object?[] { value };
    }
}
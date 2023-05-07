namespace System.Validation.Commands;

public sealed class ContainsCommand
    : ValidationCommand<string> {
    public ContainsCommand(string subString, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s.Contains(subString);
        ValidationErrorMessage = MustContain;
        Arguments = SetArguments(subString);
    }
}

public sealed class ContainsCommand<TItem>
    : ValidationCommand<ICollection<TItem?>> {
    public ContainsCommand(TItem? item, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => s.Contains(item);
        ValidationErrorMessage = MustContain;
        Arguments = SetArguments(item);
    }
}
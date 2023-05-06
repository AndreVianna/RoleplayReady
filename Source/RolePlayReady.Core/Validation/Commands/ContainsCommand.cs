namespace System.Validation.Commands;

public sealed class ContainsCommand
    : ValidationCommand<string> {

    public ContainsCommand(string subject, string subString, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.Contains(subString);
        ValidationErrorMessage = MustContain;
        Arguments = SetArguments(subString);
    }
}

public sealed class ContainsCommand<TItem>
    : ValidationCommand<ICollection<TItem?>> {

    public ContainsCommand(ICollection<TItem?> subject, TItem? item, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.Contains(item);
        ValidationErrorMessage = MustContain;
        Arguments = SetArguments(item);
    }
}
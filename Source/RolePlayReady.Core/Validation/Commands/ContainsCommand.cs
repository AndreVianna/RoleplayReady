namespace System.Validation.Commands;

public sealed class ContainsCommand
    : ValidationCommand<string> {

    public ContainsCommand(string subject, string subString, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.Contains(subString);
        ValidationErrorMessage = MustContain;
        ValidationArguments = AddArguments(subString);
    }
}

public sealed class Contains<TItem>
    : ValidationCommand<ICollection<TItem?>> {

    public Contains(ICollection<TItem?> subject, TItem? item, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        ValidateAs = s => s.Contains(item);
        ValidationErrorMessage = MustContain;
        ValidationArguments = AddArguments(item);
    }
}
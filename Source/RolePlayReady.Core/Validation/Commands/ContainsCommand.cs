namespace System.Validation.Commands;

public sealed class ContainsCommand
    : ValidationCommand {
    public ContainsCommand(string subString, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = s => ((string)s!).Contains(subString);
        ValidationErrorMessage = MustContain;
        GetArguments = _ => new object?[] { subString };
    }
}

public sealed class ContainsCommand<TItem>
    : ValidationCommand {
    public ContainsCommand(TItem? item, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = c => ((ICollection<TItem?>)c!).Contains(item);
        ValidationErrorMessage = MustContain;
        GetArguments = _ => new object?[] { item };
    }
}
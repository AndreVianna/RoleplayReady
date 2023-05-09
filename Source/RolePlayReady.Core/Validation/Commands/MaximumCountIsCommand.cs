namespace System.Validation.Commands;

public sealed class HasAtMostCommand<TItem>
    : ValidationCommand {
    public HasAtMostCommand(int count, string source, ValidationResult? validation = null)
        : base(source, validation) {
        ValidateAs = o => o is ICollection<TItem?> c && c.Count <= count;
        ValidationErrorMessage = MustHaveAMaximumCountOf;
        GetArguments = c => new object?[] { count, ((ICollection<TItem?>)c!).Count };
    }
}
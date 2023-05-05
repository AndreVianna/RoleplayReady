namespace System.Validation.Commands.Text;

public sealed class IsOneOf : ValidationCommand<string> {
    private readonly string?[] _list;

    public IsOneOf(string subject, string?[] list, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        _list = list;
    }

    public override ValidationResult Validate()
        => !_list.Contains(Subject)
            ? AddError(MustBeIn, string.Join(", ", _list), Subject)
            : Validation;
}
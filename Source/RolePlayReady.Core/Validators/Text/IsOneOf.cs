namespace System.Validators.Text;

public sealed class IsOneOf : TextValidator {
    private readonly string?[] _list;

    public IsOneOf(string source, params string?[] list)
        : base(source) {
        _list = list;
    }

    protected override ICollection<ValidationError> ValidateValue(TextValidations validation)
        => validation.IsIn(_list).Errors;
}
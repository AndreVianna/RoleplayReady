namespace System.Validators.Text;

public sealed class LengthIs : TextValidator {
    private readonly int _length;

    public LengthIs(string source, int length)
        : base(source) {
        _length = length;
    }

    protected override ICollection<ValidationError> ValidateValue(TextValidations validation)
        => validation.LengthIs(_length).Errors;
}
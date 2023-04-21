namespace System.Validators.Text;

public sealed class MaximumLengthIs : TextValidator {
    private readonly int _length;

    public MaximumLengthIs(string source, int length)
        : base(source) {
        _length = length;
    }

    protected override ValidationResult ValidateValue(TextValidation validation)
        => validation.MaximumLengthIs(_length).Result;
}
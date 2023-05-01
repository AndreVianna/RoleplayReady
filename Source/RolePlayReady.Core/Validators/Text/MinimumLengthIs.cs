namespace System.Validators.Text;

public sealed class MinimumLengthIs : TextValidator {
    private readonly int _length;

    public MinimumLengthIs(string source, int length)
        : base(source) {
        _length = length;
    }

    protected override ValidationResult ValidateValue(TextValidation validation)
        => validation.MinimumLengthIs(_length).Result;
}
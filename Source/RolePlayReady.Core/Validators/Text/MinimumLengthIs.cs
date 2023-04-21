namespace System.Validators.Text;

public sealed class MinimumLengthIs : TextValidator {
    public MinimumLengthIs(string source, int length)
        : base(source, length) {
    }

    protected override ValidationResult ValidateValue(StringValidation validation, int length)
        => validation.MinimumLengthIs(length).Result;
}
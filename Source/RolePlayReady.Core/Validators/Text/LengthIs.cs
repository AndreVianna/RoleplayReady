namespace System.Validators.Text;

public sealed class LengthIs : TextValidator {
    public LengthIs(string source, int length)
        : base(source, length) {
    }

    protected override ValidationResult ValidateValue(StringValidation validation, int length)
        => validation.LengthIs(length).Result;
}
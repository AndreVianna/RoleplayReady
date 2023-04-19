namespace System.Validators.Text;

public sealed class MaximumLengthIs : TextValidator {

    public MaximumLengthIs(string source, int length)
        : base(source, length) {
    }

    protected override ValidationResult ValidateValue(StringValidation validation, int length)
        => validation.MaximumLengthIs(length).Result;
}
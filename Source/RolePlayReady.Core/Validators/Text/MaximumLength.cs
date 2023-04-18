namespace System.Validators.Text;

public sealed class MaximumLength : TextValidator {

    public MaximumLength(string source, int length)
        : base(source, length) {
    }

    protected override ValidationResult ValidateValue(StringValidation validation, int length)
        => validation.NoShorterThan(length).Result;
}
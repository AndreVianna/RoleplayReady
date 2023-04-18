namespace System.Validators.Text;

public sealed class MinimumLength : TextValidator {

    public MinimumLength(string source, int length)
        : base(source, length) {
    }

    protected override ValidationResult ValidateValue(StringValidation validation, int length)
        => validation.NoShorterThan(length).Result;
}
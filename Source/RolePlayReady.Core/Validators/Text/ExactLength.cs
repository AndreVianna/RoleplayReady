namespace System.Validators.Text;

public sealed class ExactLength : TextValidator {

    public ExactLength(string source, int length)
        : base(source, length) {
    }

    protected override ValidationResult ValidateValue(StringValidation validation, int length)
        => validation.Exactly(length).Result;
}
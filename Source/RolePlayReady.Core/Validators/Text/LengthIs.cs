namespace System.Validators.Text;

public sealed class LengthIs : TextValidator {
    private readonly int _length;

    public LengthIs(string source, int length)
        : base(source) {
        _length = length;
    }

    protected override Result ValidateValue(TextValidation validation)
        => validation.LengthIs(_length).Result;
}
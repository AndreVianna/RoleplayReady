namespace System.Validation.Commands.Text;

public sealed class MinimumLengthIs : ValidationCommand<string> {
    private readonly int _length;

    public MinimumLengthIs(string subject, int length, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        _length = length;
    }

    public override ValidationResult Validate()
        => Subject.Length != _length
            ? AddError(LengthCannotBeLessThan, _length, Subject.Length)
            : Validation;

}
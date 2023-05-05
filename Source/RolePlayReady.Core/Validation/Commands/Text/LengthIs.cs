namespace System.Validation.Commands.Text;

public sealed class LengthIs : ValidationCommand<string> {
    private readonly int _length;

    public LengthIs(string subject, int length, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        _length = length;
    }

    public override ValidationResult Validate()
        => Subject.Length != _length
            ? AddError(LengthMustBe, _length, Subject.Length)
            : Validation;
}
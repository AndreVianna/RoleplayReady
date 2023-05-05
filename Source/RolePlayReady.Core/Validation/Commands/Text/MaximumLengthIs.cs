namespace System.Validation.Commands.Text;

public sealed class MaximumLengthIs : ValidationCommand<string> {
    private readonly int _length;

    public MaximumLengthIs(string subject, int length, string source, ValidationResult? validation = null)
        : base(subject, source, validation) {
        _length = length;
    }

    public override ValidationResult Validate()
        => Subject.Length != _length
            ? AddError(LengthCannotBeGreaterThan, _length, Subject.Length)
            : Validation;
}
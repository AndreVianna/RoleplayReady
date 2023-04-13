using System.Results.Abstractions;

namespace System.Results;

public sealed class Validation : ResultBase<Success> {
    public Validation() : base(Success.Instance) { }

    public Validation(object? input) : base(input) { }

    public static implicit operator Validation(Success _) => new();
    public static implicit operator Validation(Failure failure) => new(failure.Errors);
    public static implicit operator Validation(List<ValidationError> errors) => new(errors);
    public static implicit operator Validation(ValidationError[] errors) => new(errors);
    public static implicit operator Validation(ValidationError error) => new(error);

    public static Validation operator +(Validation left, Validation right) => (Validation)left.AddErrors(right.Errors);
}
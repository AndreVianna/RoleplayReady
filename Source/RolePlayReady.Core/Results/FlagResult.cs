using System.Extensions;

namespace System.Results;

public record FlagResult : ResultBase {
    public FlagResult(bool input) : this(Ensure.IsNotNull(input), Array.Empty<ValidationError>()) {
    }

    private FlagResult(object? input, IEnumerable<ValidationError> errors) {
        IsTrue = input switch {
            bool value => value,
            null => throw new InvalidCastException(string.Format(CannotAssignNull, nameof(FlagResult))),
            _ => throw new InvalidCastException(string.Format(CannotAssign, nameof(FlagResult), input.GetType().GetName()))
        };

        foreach (var error in errors)
            Errors.Add(error);
    }

    public bool IsTrue { get; }

    public static implicit operator FlagResult(bool value) => new(value, Array.Empty<ValidationError>());
    public static implicit operator bool(FlagResult input) => input.IsTrue;

    public static FlagResult operator +(FlagResult left, ValidationResult right) => left with { Errors = left.Errors.Union(right.Errors).ToList() };
    public static FlagResult operator +(ValidationResult left, FlagResult right) => right with { Errors = right.Errors.Union(left.Errors).ToList() };
    public static bool operator ==(FlagResult left, ValidationResult right) => ReferenceEquals(right, ValidationResult.Success) && left.IsSuccess;
    public static bool operator !=(FlagResult left, ValidationResult right) => !ReferenceEquals(right, ValidationResult.Success) || !left.IsSuccess;

    public virtual bool Equals(FlagResult? other)
        => other is not null
           && IsTrue.Equals(other.IsTrue)
           && Errors.SequenceEqual(other.Errors);

    public override int GetHashCode()
        => HashCode.Combine(base.GetHashCode(), IsTrue);
}
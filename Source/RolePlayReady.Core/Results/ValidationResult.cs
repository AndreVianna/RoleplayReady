namespace System.Results;

public sealed record ValidationResult : ResultBase {
    public ValidationResult() : this((object?)null) { }

    private ValidationResult(object? input) {
        var errors = input switch {
            IEnumerable<ValidationError> inputErrors => Ensure.IsNotNullAndHasNoNullItems(inputErrors, nameof(input)),
            ValidationError error1 => new[] { Ensure.IsNotNull(error1, nameof(input)) },
            _ => Array.Empty<ValidationError>()
        };

        foreach (var error in errors) Errors.Add(error);
    }

    public static ValidationResult Success { get; } = new();

    public static implicit operator ValidationResult(List<ValidationError> errors) => new((object?)errors);
    public static implicit operator ValidationResult(ValidationError[] errors) => new((object?)errors);
    public static implicit operator ValidationResult(ValidationError error) => new((object?)error);

    public static ValidationResult operator +(ValidationResult left, ValidationResult right) => right with { Errors = right.Errors.Union(left.Errors).ToList() };
    public static NullableResult<object> operator +(ValidationResult left, object? right) => new NullableResult<object>(right) + left;

#pragma warning disable CS8851 // Not required
    public bool Equals(ValidationResult? other) 
        => other is not null 
           && (ReferenceEquals(other, Success) 
               ? IsSuccess 
               : Errors.SequenceEqual(other.Errors));
#pragma warning restore CS8851
}
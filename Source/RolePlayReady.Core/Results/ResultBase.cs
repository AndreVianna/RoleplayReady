namespace System.Results;

public abstract record ResultBase : IResult {
    public bool IsSuccess => Errors.Count == 0;
    public bool HasErrors => Errors.Count != 0;
    public ICollection<ValidationError> Errors { get; protected init; } = new List<ValidationError>();

    public override int GetHashCode()
        => Errors.Aggregate(Array.Empty<ValidationError>().GetHashCode(), HashCode.Combine);
}
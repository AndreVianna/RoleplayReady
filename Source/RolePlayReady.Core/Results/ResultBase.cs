﻿namespace System.Results;

public abstract record ResultBase : IResult {
    public virtual bool IsSuccess => Errors.Count == 0;
    public bool HasErrors => Errors.Count != 0;
    public IList<ValidationError> Errors { get; protected init; } = new List<ValidationError>();

    public override int GetHashCode()
        => Errors.Aggregate(Array.Empty<ValidationError>().GetHashCode(), HashCode.Combine);
}
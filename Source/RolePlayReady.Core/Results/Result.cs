﻿namespace System.Results;

public abstract record Result : IResult {
    protected Result(IEnumerable<ValidationError>? errors = null) {
        Errors = errors?.ToList() ?? [];
    }

    public IList<ValidationError> Errors { get; protected init; } = [];
    public bool IsInvalid => Errors.Count != 0;
    public abstract bool IsSuccess { get; }

    public virtual bool Equals(Result? other) => other is not null
                                                         && Errors.SequenceEqual(other.Errors);

    public override int GetHashCode() => Errors.Aggregate(Array.Empty<ValidationError>().GetHashCode(), HashCode.Combine);
}
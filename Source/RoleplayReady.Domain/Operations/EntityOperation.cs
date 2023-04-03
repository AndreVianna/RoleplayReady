using RolePlayReady.Models.Contracts;

namespace RolePlayReady.Operations;

public abstract record EntityOperation<TOperation, TResult>
    : IEntityOperation<TOperation, TResult>
    where TOperation : IEntityOperation<TOperation, TResult> {
    public TOperation? Next { get; init; }
    public abstract TResult Run(IEntity original);
}
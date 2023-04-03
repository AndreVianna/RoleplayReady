using RolePlayReady.Models.Contracts;

namespace RolePlayReady.Operations;

public interface IEntityOperation {
}

public interface IEntityOperation<TOperation, out TResult> : IEntityOperation
    where TOperation : IEntityOperation<TOperation, TResult> {
    TOperation? Next { get; init; }
    TResult Run(IEntity original);
}

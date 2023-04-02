namespace RoleplayReady.Domain.Operations.Changes;

public interface IEntityChange
    : IEntityOperation<IEntityChange, IEntity> {
    Func<IEntity, IEntity> Apply { get; init; }
}
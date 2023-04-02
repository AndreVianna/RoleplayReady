namespace RoleplayReady.Domain.Operations.Assertions;

public interface IEntityAssertion
    : IEntityOperation<IEntityAssertion, bool> {
    Func<IEntity, bool> IsTrue { get; init; }
}
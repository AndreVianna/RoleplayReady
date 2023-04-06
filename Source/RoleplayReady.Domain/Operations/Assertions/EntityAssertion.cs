using RolePlayReady.Utilities;

namespace RolePlayReady.Operations.Assertions;

public abstract record EntityAssertion
    : EntityOperation<IEntityAssertion, bool>,
      IEntityAssertion {
    protected EntityAssertion() { }

    [SetsRequiredMembers]
    protected EntityAssertion(Func<IEntity, bool> isTrue) {
        IsTrue = Throw.IfNull(isTrue);
    }

    public required Func<IEntity, bool> IsTrue { get; init; }

    public override bool Run(IEntity original) {
        var result = IsTrue(original);
        return result && (Next?.Run(original) ?? false);
    }
}
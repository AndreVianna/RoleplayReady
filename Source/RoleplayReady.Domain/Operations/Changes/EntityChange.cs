namespace RoleplayReady.Domain.Operations.Changes;

public abstract record EntityChange
    : EntityOperation<IEntityChange, IEntity>,
      IEntityChange {
    protected EntityChange() { }

    [SetsRequiredMembers]
    protected EntityChange(Func<IEntity, IEntity> apply) {
        Apply = apply;
    }

    public required Func<IEntity, IEntity> Apply { get; init; }

    public override IEntity Run(IEntity original) {
        var result = Apply(original);
        return Next?.Run(result) ?? result;
    }
}
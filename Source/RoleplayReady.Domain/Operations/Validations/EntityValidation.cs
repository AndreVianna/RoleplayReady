namespace RolePlayReady.Operations.Validations;

public abstract record EntityValidation
    : EntityOperation<IEntityValidation, IList<ValidationError>>,
      IEntityValidation {
    protected EntityValidation() { }

    [SetsRequiredMembers]
    protected EntityValidation(Func<IEntity, IList<ValidationError>, IList<ValidationError>> apply) {
        Assert = apply;
    }

    public required Func<IEntity, IList<ValidationError>, IList<ValidationError>> Assert { get; init; }

    public override IList<ValidationError> Run(IEntity original)
        => Assert(original, Next?.Run(original) ?? new List<ValidationError>());
}

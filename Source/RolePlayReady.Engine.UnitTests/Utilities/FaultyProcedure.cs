namespace RolePlayReady.Engine.Utilities;


internal class FaultyProcedure : DefaultProcedure {
    [SetsRequiredMembers]
    public FaultyProcedure(bool throwsOnError = true)
        : base(new EmptyContext(throwsOnError)) { }

    protected override Task<Type?> OnStartAsync(CancellationToken cancellation = default)
        => throw new Exception("Some exception.");
}
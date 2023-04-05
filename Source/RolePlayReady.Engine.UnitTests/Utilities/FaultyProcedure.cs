namespace RolePlayReady.Engine.Utilities;


internal class FaultyProcedure : DefaultProcedure {
    public FaultyProcedure() : base(NullStepFactory.Instance) {
    }

    protected override Task<Type?> OnStartAsync(CancellationToken cancellation = default)
        => throw new Exception("Some exception.");
}
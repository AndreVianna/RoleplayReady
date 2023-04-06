namespace RolePlayReady.Engine.Utilities;


internal class FaultyProcedure : DefaultProcedure {
    public FaultyProcedure(DefaultContext context, IStepFactory stepFactory) : base(context, stepFactory) {
    }

    protected override Task<Type?> OnStartAsync(CancellationToken cancellation = default)
        => throw new Exception("Some exception.");
}
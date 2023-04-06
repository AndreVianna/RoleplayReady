namespace RolePlayReady.Engine.Utilities;

internal class FaultyStep : Step<NullContext> {
    public FaultyStep(IStepFactory stepFactory) : base(stepFactory, null) { }


    protected override Task<Type?> OnRunAsync(NullContext context, CancellationToken cancellation = default)
        => throw new Exception("Some exception.");
}
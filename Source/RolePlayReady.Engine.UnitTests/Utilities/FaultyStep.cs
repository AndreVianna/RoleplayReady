namespace RolePlayReady.Engine.Utilities;

internal class FaultyStep : Step {
    public FaultyStep(IStepFactory stepFactory) : base(stepFactory) { }


    protected override Task<Type?> OnRunAsync(CancellationToken cancellation = default)
        => throw new Exception("Some exception.");
}
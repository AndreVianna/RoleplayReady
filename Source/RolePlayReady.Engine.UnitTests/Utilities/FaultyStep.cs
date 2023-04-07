namespace RolePlayReady.Engine.Utilities;

internal class FaultyStep : Step<NullContext> {
    [SetsRequiredMembers]
    public FaultyStep(IStepFactory stepFactory)
    : base(stepFactory, null) {
    }

    protected override Task<NullContext> OnStartAsync(NullContext context, CancellationToken cancellation = default)
        => throw new Exception("Some exception.");
}
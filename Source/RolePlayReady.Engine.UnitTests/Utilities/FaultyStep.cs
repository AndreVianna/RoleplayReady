namespace RolePlayReady.Engine.Utilities;

[method: SetsRequiredMembers]
internal class FaultyStep(IStepFactory stepFactory) : Step<NullContext>(stepFactory, null) {
    protected override Task<NullContext> OnStartAsync(NullContext context, CancellationToken cancellation = default) => throw new("Some exception.");
}
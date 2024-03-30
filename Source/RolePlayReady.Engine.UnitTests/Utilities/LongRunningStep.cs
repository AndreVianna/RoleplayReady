namespace RolePlayReady.Engine.Utilities;

internal class LongRunningStep(IStepFactory stepFactory) : Step<NullContext>(stepFactory, null) {
    protected override async Task<NullContext> OnStartAsync(NullContext context, CancellationToken cancellation = default) {
        await Task.Delay(TimeSpan.FromSeconds(10), cancellation);
        return await base.OnStartAsync(context, cancellation);
    }
}
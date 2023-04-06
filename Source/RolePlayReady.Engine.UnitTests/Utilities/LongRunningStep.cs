namespace RolePlayReady.Engine.Utilities;

internal class LongRunningStep : Step<NullContext> {
    public LongRunningStep(IStepFactory stepFactory) : base(stepFactory, null) { }

    protected override async Task<Type?> OnRunAsync(NullContext context, CancellationToken cancellation = default) {
        await Task.Delay(TimeSpan.FromSeconds(10), cancellation);
        return await base.OnRunAsync(context, cancellation);
    }
}
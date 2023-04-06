namespace RolePlayReady.Engine.Utilities;

internal class LongRunningStep : Step {
    public LongRunningStep(IStepFactory stepFactory) : base(stepFactory) { }

    protected override async Task<Type?> OnRunAsync(CancellationToken cancellation = default) {
        await Task.Delay(TimeSpan.FromSeconds(10), cancellation);
        return await base.OnRunAsync(cancellation);
    }
}
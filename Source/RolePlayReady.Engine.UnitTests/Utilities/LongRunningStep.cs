using RolePlayReady.Engine.Steps.Abstractions;

namespace RolePlayReady.Engine.Utilities;

internal class LongRunningStep : Step<NullContext> {
    public LongRunningStep(IStepFactory stepFactory) : base(stepFactory, null) { }

    protected override async Task<NullContext> OnStartAsync(NullContext context, CancellationToken cancellation = default) {
        await Task.Delay(TimeSpan.FromSeconds(10), cancellation);
        return await base.OnStartAsync(context, cancellation);
    }
}
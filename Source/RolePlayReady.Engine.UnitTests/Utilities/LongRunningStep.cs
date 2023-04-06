namespace RolePlayReady.Engine.Utilities;

internal class LongRunningStep<TContext> : Step<TContext>
    where TContext : class, IContext {
    public LongRunningStep(IStepFactory stepFactory) : base(stepFactory) { }

    protected override async Task<Type?> OnRunAsync(TContext context, CancellationToken cancellation = default) {
        await Task.Delay(TimeSpan.FromSeconds(10), cancellation);
        return await base.OnRunAsync(context, cancellation);
    }
}
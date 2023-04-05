namespace RolePlayReady.Engine.Utilities;

internal class LongRunningStep : Step {
    public LongRunningStep()
        : this(null, null) { }

    public LongRunningStep(IStepFactory? stepFactory, ILoggerFactory? loggerFactory)
        : base(stepFactory, loggerFactory) { }

    protected override async Task<Type?> OnRunAsync(CancellationToken cancellation = default) {
        await Task.Delay(TimeSpan.FromSeconds(10), cancellation);
        return await base.OnRunAsync(cancellation);
    }
}
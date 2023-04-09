namespace RolePlayReady.Engine.Utilities;

internal class LongRunningStepRunner : StepRunner<NullContext, RunnerOptions> {
    [SetsRequiredMembers]
    public LongRunningStepRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory)
        : base(configuration, stepFactory, loggerFactory) {
    }

    protected override async Task<NullContext> OnStartAsync(NullContext context, CancellationToken cancellation = default) {
        await Task.Delay(TimeSpan.FromSeconds(10), cancellation);
        return await base.OnStartAsync(context, cancellation);
    }
}
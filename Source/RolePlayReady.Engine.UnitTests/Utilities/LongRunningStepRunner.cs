namespace RolePlayReady.Engine.Utilities;

[method: SetsRequiredMembers]
internal class LongRunningStepRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory) : StepRunner<NullContext, RunnerOptions>(configuration, stepFactory, loggerFactory) {
    protected override async Task<NullContext> OnStartAsync(NullContext context, CancellationToken cancellation = default) {
        await Task.Delay(TimeSpan.FromSeconds(10), cancellation);
        return await base.OnStartAsync(context, cancellation);
    }
}
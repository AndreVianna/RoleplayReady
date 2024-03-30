namespace RolePlayReady.Engine.Utilities;

[method: SetsRequiredMembers]
internal class LongRunningRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory) : SimpleRunner<NullContext, RunnerOptions>(configuration, stepFactory, loggerFactory) {
    protected override async Task<NullContext> OnRunAsync(NullContext context, CancellationToken cancellation = default) {
        await Task.Delay(TimeSpan.FromSeconds(10), cancellation);
        return await base.OnRunAsync(context, cancellation);
    }
}
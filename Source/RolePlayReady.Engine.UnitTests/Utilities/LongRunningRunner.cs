using RolePlayReady.Engine.Steps.Abstractions;

namespace RolePlayReady.Engine.Utilities;

internal class LongRunningRunner : SimpleRunner<NullContext, RunnerOptions> {
    [SetsRequiredMembers]
    public LongRunningRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory)
        : base(configuration, stepFactory, loggerFactory) {
    }

    protected override async Task<NullContext> OnRunAsync(NullContext context, CancellationToken cancellation = default) {
        await Task.Delay(TimeSpan.FromSeconds(10), cancellation);
        return await base.OnRunAsync(context, cancellation);
    }
}
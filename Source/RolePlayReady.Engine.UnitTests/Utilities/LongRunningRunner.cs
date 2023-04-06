namespace RolePlayReady.Engine.Utilities;

internal class LongRunningRunner : Runner<NullContext, RunnerOptions> {
    [SetsRequiredMembers]
    public LongRunningRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory)
        : base(configuration, stepFactory, loggerFactory) {
    }

    protected override async Task<Type?> OnStartAsync(NullContext context, CancellationToken cancellation = default) {
        await Task.Delay(TimeSpan.FromSeconds(10), cancellation);
        return default;
    }
}
namespace RolePlayReady.Engine.Utilities;

internal class FaultyRunner : SimpleRunner<NullContext, RunnerOptions> {
    [SetsRequiredMembers]
    public FaultyRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory)
        : base(configuration, stepFactory, loggerFactory) {
    }

    protected override Task<NullContext> OnRunAsync(NullContext context, CancellationToken cancellation = default)
        => throw new Exception("Some exception.");
}
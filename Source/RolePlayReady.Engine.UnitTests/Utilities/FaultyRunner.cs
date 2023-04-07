namespace RolePlayReady.Engine.Utilities;

internal class FaultyRunner : StepRunner<NullContext, RunnerOptions> {
    [SetsRequiredMembers]
    public FaultyRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory)
        : base(configuration, stepFactory, loggerFactory) {
    }

    protected override Task<NullContext> OnStartAsync(NullContext context, CancellationToken cancellation = default)
        => throw new Exception("Some exception.");
}
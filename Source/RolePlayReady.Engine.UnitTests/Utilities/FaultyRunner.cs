namespace RolePlayReady.Engine.Utilities;

[method: SetsRequiredMembers]
internal class FaultyRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory) : SimpleRunner<NullContext, RunnerOptions>(configuration, stepFactory, loggerFactory) {
    protected override Task<NullContext> OnRunAsync(NullContext context, CancellationToken cancellation = default) => throw new("Some exception.");
}
namespace RolePlayReady.Engine.Utilities;

[method: SetsRequiredMembers]
internal class FaultyStepRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory) : StepRunner<NullContext, RunnerOptions>(configuration, stepFactory, loggerFactory) {
    protected override Task<NullContext> OnStartAsync(NullContext context, CancellationToken cancellation = default) => throw new("Some exception.");
}
using RolePlayReady.Engine.Steps.Abstractions;

namespace RolePlayReady.Engine.Utilities;

internal class FaultyStepRunner : StepRunner<NullContext, RunnerOptions> {
    [SetsRequiredMembers]
    public FaultyStepRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory)
        : base(configuration, stepFactory, loggerFactory) {
    }

    protected override Task<NullContext> OnStartAsync(NullContext context, CancellationToken cancellation = default)
        => throw new("Some exception.");
}
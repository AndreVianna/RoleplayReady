using RolePlayReady.Engine.Steps.Abstractions;

namespace RolePlayReady.Engine.Utilities;

internal class TestSimpleRunner : SimpleRunner<Context, TestRunnerOptions> {
    [SetsRequiredMembers]
    public TestSimpleRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory)
        : base(configuration, stepFactory, loggerFactory) {
    }

    public Task<Context> TestOnRunAsync(Context context, CancellationToken cancellation = default)
        => OnRunAsync(context, cancellation);

    public Task TestOnErrorAsync(Exception ex, Context context, CancellationToken cancellation = default)
        => OnErrorAsync(ex, context, cancellation);
}
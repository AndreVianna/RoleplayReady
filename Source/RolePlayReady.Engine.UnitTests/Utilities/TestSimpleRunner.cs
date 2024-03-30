namespace RolePlayReady.Engine.Utilities;

[method: SetsRequiredMembers]
internal class TestSimpleRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory) : SimpleRunner<Context, TestRunnerOptions>(configuration, stepFactory, loggerFactory) {
    public Task<Context> TestOnRunAsync(Context context, CancellationToken cancellation = default) => OnRunAsync(context, cancellation);

    public Task TestOnErrorAsync(Exception ex, Context context, CancellationToken cancellation = default) => OnErrorAsync(ex, context, cancellation);
}
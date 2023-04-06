namespace RolePlayReady.Engine.Utilities;

internal class TestRunnerOptions : RunnerOptions<TestRunnerOptions> {
}

internal class TestRunner : Runner<Context, TestRunnerOptions> {
    [SetsRequiredMembers]
    public TestRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory)
        : base(configuration, stepFactory, loggerFactory) {
    }

    protected override Task<Type?> OnStartAsync(Context context, CancellationToken cancellation = default)
        => Task.FromResult<Type?>(typeof(TestStep));

    public Task<Type?> TestOnStartAsync(Context context, CancellationToken cancellation = default)
        => OnStartAsync(context, cancellation);

    public Task TestOnFinishAsync(Context context, CancellationToken cancellation = default)
        => OnFinishAsync(context, cancellation);

    public Task TestOnErrorAsync(Exception ex, Context context, CancellationToken cancellation = default)
        => OnErrorAsync(ex, context, cancellation);
}
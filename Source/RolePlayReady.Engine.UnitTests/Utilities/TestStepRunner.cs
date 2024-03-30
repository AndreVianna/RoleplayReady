namespace RolePlayReady.Engine.Utilities;

[method: SetsRequiredMembers]
internal class TestStepRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory) : StepRunner<Context, TestRunnerOptions>(configuration, stepFactory, loggerFactory) {
    protected override Task<Type?> OnSelectStepAsync(Context context, CancellationToken cancellation = default) => Task.FromResult<Type?>(typeof(TestStep));

    public Task<Type?> TestOnSelectStepAsync(Context context, CancellationToken cancellation = default) => base.OnSelectStepAsync(context, cancellation);

    public Task<Context> TestOnStartAsync(Context context, CancellationToken cancellation = default) => OnStartAsync(context, cancellation);

    public Task<Context> TestOnFinishAsync(Context context, CancellationToken cancellation = default) => OnFinishAsync(context, cancellation);

    public Task TestOnErrorAsync(Exception ex, Context context, CancellationToken cancellation = default) => OnErrorAsync(ex, context, cancellation);
}
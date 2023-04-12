using RolePlayReady.Engine.Steps.Abstractions;

namespace RolePlayReady.Engine.Utilities;

internal class TestStepRunner : StepRunner<Context, TestRunnerOptions> {
    [SetsRequiredMembers]
    public TestStepRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory)
        : base(configuration, stepFactory, loggerFactory) {
    }

    protected override Task<Type?> OnSelectStepAsync(Context context, CancellationToken cancellation = default)
        => Task.FromResult<Type?>(typeof(TestStep));

    public Task<Type?> TestOnSelectStepAsync(Context context, CancellationToken cancellation = default)
        => base.OnSelectStepAsync(context, cancellation);

    public Task<Context> TestOnStartAsync(Context context, CancellationToken cancellation = default)
        => OnStartAsync(context, cancellation);

    public Task<Context> TestOnFinishAsync(Context context, CancellationToken cancellation = default)
        => OnFinishAsync(context, cancellation);

    public Task TestOnErrorAsync(Exception ex, Context context, CancellationToken cancellation = default)
        => OnErrorAsync(ex, context, cancellation);
}
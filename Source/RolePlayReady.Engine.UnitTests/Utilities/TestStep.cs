using RolePlayReady.Engine.Steps.Abstractions;

namespace RolePlayReady.Engine.Utilities;

internal class TestStep : Step<Context> {
    public TestStep(IStepFactory stepFactory, ILoggerFactory? loggerFactory) : base(stepFactory, loggerFactory) { }

    protected override Task<Type?> OnSelectNextAsync(Context context, CancellationToken cancellation = default)
        => Task.FromResult<Type?>(typeof(EndStep<Context>));

    public Task TestOnFinishAsync(Context context, CancellationToken cancellation = default)
        => OnFinishAsync(context, cancellation);

    public Task TestOnErrorAsync(Exception ex, Context context, CancellationToken cancellation = default)
        => OnErrorAsync(ex, context, cancellation);
}
namespace RolePlayReady.Engine.Steps;

public class EndStep<TContext> : Step<TContext>
    where TContext : class, IContext {
    public EndStep(IStepFactory stepFactory, ILoggerFactory? loggerFactory = null)
        : base(stepFactory, loggerFactory) {
    }

    protected sealed override Task<TContext> OnStartAsync(TContext context, CancellationToken cancellation = default)
        => base.OnStartAsync(context, cancellation);

    protected sealed override Task<Type?> OnSelectNextAsync(TContext context, CancellationToken cancellation = default)
        => base.OnSelectNextAsync(context, cancellation);

    protected sealed override Task<TContext> OnFinishAsync(TContext context, CancellationToken cancellation = default)
        => base.OnFinishAsync(context, cancellation);

    protected sealed override Task OnErrorAsync(Exception ex, TContext context, CancellationToken cancellation = default)
        => base.OnErrorAsync(ex, context, cancellation);
}
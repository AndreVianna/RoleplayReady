namespace RolePlayReady.Engine.Steps;

public class EndStep<TContext> : Step<TContext>
    where TContext : class, IContext {

    public EndStep(IStepFactory stepFactory, ILoggerFactory? loggerFactory = null)
        : base(stepFactory, loggerFactory) {
    }

    protected sealed override Task<Type?> OnRunAsync(TContext context, CancellationToken cancellation = default)
        => base.OnRunAsync(context, cancellation);

    protected sealed override Task OnFinishAsync(TContext context, CancellationToken cancellation = default)
        => base.OnFinishAsync(context, cancellation);

    protected sealed override Task OnErrorAsync(Exception ex, TContext context, CancellationToken cancellation = default)
        => base.OnErrorAsync(ex, context, cancellation);
}
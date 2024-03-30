namespace RolePlayReady.Engine.Steps;

public class EndStep<TContext>(IStepFactory stepFactory, ILoggerFactory? loggerFactory = null) : Step<TContext>(stepFactory, loggerFactory)
    where TContext : class, IContext {
    protected sealed override Task<TContext> OnStartAsync(TContext context, CancellationToken cancellation = default) => base.OnStartAsync(context, cancellation);

    protected sealed override Task<Type?> OnSelectNextAsync(TContext context, CancellationToken cancellation = default) => base.OnSelectNextAsync(context, cancellation);

    protected sealed override Task<TContext> OnFinishAsync(TContext context, CancellationToken cancellation = default) => base.OnFinishAsync(context, cancellation);

    protected sealed override Task OnErrorAsync(Exception ex, TContext context, CancellationToken cancellation = default) => base.OnErrorAsync(ex, context, cancellation);
}
namespace RolePlayReady.Engine.Steps;

public class EndStep<TContext> : Step<TContext>
    where TContext : DefaultContext {

    public EndStep(IServiceCollection services, ILoggerFactory? loggerFactory = null)
        : base(services, loggerFactory) {
    }

    protected sealed override Task<Type?> OnRunAsync(CancellationToken cancellation = default)
        => base.OnRunAsync(cancellation);

    protected sealed override Task OnFinishAsync(CancellationToken cancellation = default)
        => base.OnFinishAsync(cancellation);

    protected sealed override Task OnErrorAsync(Exception ex, CancellationToken cancellation = default)
        => base.OnErrorAsync(ex, cancellation);
}
namespace RolePlayReady.Engine;

public class EndStep : EndStep<EmptyContext> {
    public EndStep(IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(stepFactory, loggerFactory) {
    }
}

public class EndStep<TContext> : Step<TContext>
    where TContext : EmptyContext {

    public EndStep(IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(stepFactory, loggerFactory) {
    }

    protected sealed override Task<Type?> OnRunAsync(CancellationToken cancellation = default)
        => base.OnRunAsync(cancellation);

    protected sealed override Task OnFinishAsync(CancellationToken cancellation = default)
        => base.OnFinishAsync(cancellation);

    protected sealed override Task OnErrorAsync(Exception ex, CancellationToken cancellation = default)
        => base.OnErrorAsync(ex, cancellation);
}
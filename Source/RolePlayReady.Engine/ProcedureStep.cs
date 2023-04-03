namespace RolePlayReady.Engine;

public abstract record ProcedureStep<TContext> : IValidatable, IAsyncDisposable
    where TContext : ProcedureContext<TContext> {
    private readonly IStepFactory _stepFactory;
    private readonly Type _stepType;

    protected ProcedureStep(IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null) {
        _stepFactory = stepFactory ?? new StepFactory();
        _stepType = GetType();
        Logger = loggerFactory?.CreateLogger(_stepType) ?? NullLogger.Instance;
    }

    public virtual ValidationResult Validate<TValidationContext>(TValidationContext? context = null)
        where TValidationContext : class => ValidationResult.Valid;

    protected ILogger Logger { get; }
    protected Type? NextStep { get; set; }
    protected virtual Task OnRunAsync(CancellationToken cancellation) => Task.CompletedTask;
    protected virtual Task OnFinishAsync(CancellationToken cancellation) => Task.CompletedTask;
    protected virtual Task OnErrorAsync(Exception ex, CancellationToken cancellation) => Task.CompletedTask;

    public async Task RunAsync(TContext context, CancellationToken cancellation) {
        try {
            context.IncrementStepNumber();
            Logger.LogDebug("Starting step {StepNumber}: {Type}...", context.StepNumber, _stepType.Name);
            await OnRunAsync(cancellation);
            if (NextStep is null)
                throw new InvalidOperationException("The next step was not set.");
            Logger.LogDebug("Running step {StepNumber}: {Type}...", context.StepNumber, _stepType.Name);
            await using var next = _stepFactory.Create<TContext>(NextStep);
            await next.RunAsync(context, cancellation);
            await next.DisposeAsync();
            if (cancellation.IsCancellationRequested) {
                Logger.LogDebug("Step {StepNumber}: {Type} cancelled.", context.StepNumber, _stepType.Name);
                return;
            }

            Logger.LogDebug("Finishing step {StepNumber}: {Type}...", context.StepNumber, _stepType.Name);
            await OnFinishAsync(cancellation);
            Logger.LogDebug("Step {StepNumber}: {Type} finished.", context.StepNumber, _stepType.Name);
        }
        catch (Exception ex) {
            await OnErrorAsync(ex, cancellation);
            Logger.LogError(ex, "An error has occurred while executing step {StepNumber}: {Type}.", context.StepNumber, _stepType.Name);
            if (context.ThrowOnError)
                throw;
        }
    }

    private bool _disposed;
    protected virtual ValueTask DisposeAsync(bool _) {
        if (_disposed)
            return ValueTask.CompletedTask;

        _disposed = true;
        return ValueTask.CompletedTask;
    }

    public async ValueTask DisposeAsync() {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}

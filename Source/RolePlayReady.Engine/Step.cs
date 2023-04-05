namespace RolePlayReady.Engine;

public abstract class Step : Step<EmptyContext> {

    protected Step(IStepFactory? stepFactory, ILoggerFactory? loggerFactory)
        : base(stepFactory, loggerFactory) {
    }

    public Task RunAsync(CancellationToken cancellation = default) => RunAsync(true, cancellation);
    public Task RunAsync(bool throwsOnError, CancellationToken cancellation = default) => base.RunAsync(new(throwsOnError), cancellation);
}

public abstract class Step<TContext> : IAsyncDisposable, IStep<TContext>
    where TContext : EmptyContext {
    private readonly Type _stepType;
    private readonly IStepFactory _stepFactory;
    private readonly ILogger _logger;

    protected Step(IStepFactory? stepFactory, ILoggerFactory? loggerFactory) {
        _stepType = GetType();
        _stepFactory = stepFactory ?? new StepFactory();
        _logger = loggerFactory?.CreateLogger(_stepType) ?? NullLogger.Instance;
    }

    protected virtual Task<Type?> OnRunAsync(CancellationToken cancellation = default)
        => Task.FromResult((Type?)null);

    protected virtual Task OnFinishAsync(CancellationToken cancellation = default)
        => Task.CompletedTask;

    protected virtual Task OnErrorAsync(Exception ex, CancellationToken cancellation = default)
        => Task.CompletedTask;

    public bool IsRunning { get; private set; }

    Task IStep.RunAsync(EmptyContext context, CancellationToken cancellation) => RunAsync((TContext)context, cancellation);
    public async Task RunAsync(TContext context, CancellationToken cancellation = default) {
        try {
            if (IsRunning) return;
            IsRunning = true;
            context.CurrentStepNumber++;
            context.CurrentStepType = _stepType;
            _logger.LogDebug("Running step {StepNumber}: '{Type}'...", context.CurrentStepNumber, _stepType.Name);
            var nextStepType = await OnRunAsync(cancellation).ConfigureAwait(false);
            if (nextStepType is not null) {
                _logger.LogDebug("Expecting next step of type '{NextStepType}'.", nextStepType.Name);
                await using var nextStep = _stepFactory.Create<TContext>(nextStepType);
                await nextStep.RunAsync(context, cancellation).ConfigureAwait(false);
            }

            _logger.LogDebug("Finishing step {StepNumber}: '{Type}'...", context.CurrentStepNumber, _stepType.Name);
            await OnFinishAsync(cancellation).ConfigureAwait(false);
            _logger.LogDebug("Step {StepNumber}: '{Type}' finished.", context.CurrentStepNumber, _stepType.Name);
        }
        catch (OperationCanceledException ex) {
            _logger.LogError(ex, "Step {StepNumber}: '{Type}' cancelled.", context.CurrentStepNumber, _stepType.Name);
            if (context.ThrowsOnError) throw;
        }
        catch (Exception ex) {
            _logger.LogError(ex, "An error has occurred while executing step {StepNumber}: {Type}.", context.CurrentStepNumber, _stepType.Name);
            await OnErrorAsync(ex, cancellation).ConfigureAwait(false);
            if (context.ThrowsOnError)
                throw new ProcedureException($"An error has occurred while executing step {context.CurrentStepNumber}: {_stepType.Name}.", ex);
        }
        finally {
            IsRunning = false;
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
        await DisposeAsync(true).ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }
}

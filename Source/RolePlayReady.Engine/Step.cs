namespace RolePlayReady.Engine;

public abstract class Step<TContext> : IStep<TContext>
    where TContext : class, IContext {
    private readonly Type _stepType;
    private readonly IStepFactory _stepFactory;
    private readonly ILogger _logger;

    protected Step(IStepFactory stepFactory, ILoggerFactory? loggerFactory) {
        _stepType = GetType();
        _logger = loggerFactory?.CreateLogger(_stepType) ?? NullLogger.Instance;
        _stepFactory = stepFactory;
    }

    protected virtual Task<TContext> OnStartAsync(TContext context, CancellationToken cancellation = default)
        => Task.FromResult(context);
    protected virtual Task<Type?> OnSelectNextAsync(TContext context, CancellationToken cancellation = default)
        => Task.FromResult<Type?>(default);
    protected virtual Task<TContext> OnFinishAsync(TContext context, CancellationToken cancellation = default)
        => Task.FromResult(context);
    protected virtual Task OnErrorAsync(Exception ex, TContext context, CancellationToken cancellation = default)
        => Task.CompletedTask;

    async Task<IContext> IStep.RunAsync(IContext context, CancellationToken cancellation)
        => await RunAsync(
            Throw.IfNull(context as TContext, $"Context must be of type '{typeof(TContext).Name}'."),
            cancellation);
    public async Task<TContext> RunAsync(TContext context, CancellationToken cancellation = default) {
        try {
            Throw.IfNull(context);
            await context.UpdateAsync(this, cancellation).ConfigureAwait(false);

            _logger.LogDebug("Running step {StepNumber}: '{Type}'...", context.CurrentStepNumber, _stepType.Name);
            context = await OnStartAsync(context, cancellation).ConfigureAwait(false);

            var nextStepType = await OnSelectNextAsync(context, cancellation).ConfigureAwait(false);
            if (nextStepType is not null) {
                _logger.LogDebug("Expecting next step of type '{NextStepType}'.", nextStepType.Name);
                await using var nextStep = _stepFactory.Create(nextStepType);
                context = (TContext)await nextStep.RunAsync(context, cancellation).ConfigureAwait(false);
            }

            _logger.LogDebug("Finishing step {StepNumber}: '{Type}'...", context.CurrentStepNumber, _stepType.Name);
            context = await OnFinishAsync(context, cancellation).ConfigureAwait(false);
            _logger.LogDebug("Step {StepNumber}: '{Type}' finished.", context.CurrentStepNumber, _stepType.Name);

            return context;
        }
        catch (OperationCanceledException ex) {
            _logger.LogError(ex, "Step {StepNumber}: '{Type}' cancelled.", context.CurrentStepNumber, _stepType.Name);
            throw;
        }
        catch (Exception ex) {
            _logger.LogError(ex, "An error has occurred while executing step {StepNumber}: {Type}.", context.CurrentStepNumber, _stepType.Name);
            await OnErrorAsync(ex, context, cancellation).ConfigureAwait(false);
            throw new ProcedureException($"An error has occurred while executing step {context.CurrentStepNumber}: {_stepType.Name}.", ex);
        }
    }

    private bool _disposed;
    protected virtual ValueTask DisposeAsync(bool disposing) {
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

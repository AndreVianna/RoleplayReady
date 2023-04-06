﻿namespace RolePlayReady.Engine;

public abstract class Step<TContext> : IStep<TContext>
    where TContext : class, IContext {
    private readonly Type _stepType;
    private readonly IStepFactory _stepFactory;
    private readonly ILogger _logger;

    protected Step(IStepFactory stepFactory, ILoggerFactory? loggerFactory = null) {
        _stepType = GetType();
        _logger = loggerFactory?.CreateLogger(_stepType) ?? NullLogger.Instance;
        _stepFactory = stepFactory;
    }

    protected virtual Task<Type?> OnRunAsync(TContext context, CancellationToken cancellation = default)
        => Task.FromResult<Type?>(default);

    protected virtual Task OnFinishAsync(TContext context, CancellationToken cancellation = default)
        => Task.CompletedTask;

    protected virtual Task OnErrorAsync(Exception ex, TContext context, CancellationToken cancellation = default)
        => Task.CompletedTask;

    Task IStep.RunAsync(IContext context, CancellationToken cancellation)
        => RunAsync(
            Throw.IfNull(context as TContext, $"Context must be of type '{typeof(TContext).Name}'."),
            cancellation);
    public async Task RunAsync(TContext context, CancellationToken cancellation = default) {
        try {
            Throw.IfNull(context);
            context.CurrentStepNumber++;
            context.CurrentStepType = _stepType;
            _logger.LogDebug("Running step {StepNumber}: '{Type}'...", context.CurrentStepNumber, _stepType.Name);
            var nextStepType = await OnRunAsync(context, cancellation).ConfigureAwait(false);
            if (nextStepType is not null) {
                _logger.LogDebug("Expecting next step of type '{NextStepType}'.", nextStepType.Name);
                await using var nextStep = _stepFactory.Create(nextStepType);
                await nextStep.RunAsync(context, cancellation).ConfigureAwait(false);
            }

            _logger.LogDebug("Finishing step {StepNumber}: '{Type}'...", context.CurrentStepNumber, _stepType.Name);
            await OnFinishAsync(context, cancellation).ConfigureAwait(false);
            _logger.LogDebug("Step {StepNumber}: '{Type}' finished.", context.CurrentStepNumber, _stepType.Name);
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

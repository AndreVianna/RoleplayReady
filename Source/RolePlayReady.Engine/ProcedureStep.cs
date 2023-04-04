using System.Xml.Linq;

namespace RolePlayReady.Engine;

public abstract class ProcedureStep<TContext> : IAsyncDisposable
    where TContext : ProcedureContext<TContext> {
    private readonly Type _stepType;
    private readonly IStepFactory _stepFactory;
    private readonly ILogger _logger;

    protected ProcedureStep(IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null) {
        _stepType = GetType();
        _stepFactory = stepFactory ?? new StepFactory();
        _logger = loggerFactory?.CreateLogger(_stepType) ?? NullLogger.Instance;
    }

    protected Type? NextStep { get; set; }
    protected virtual Task OnRunAsync(CancellationToken cancellation = default) => Task.CompletedTask;
    protected virtual Task OnFinishAsync(CancellationToken cancellation = default) => Task.CompletedTask;
    protected virtual Task OnErrorAsync(Exception ex, CancellationToken cancellation = default) => Task.CompletedTask;

    public async Task RunAsync(TContext context, CancellationToken cancellation = default) {
        try {
            context.IncrementStepNumber();
            _logger.LogDebug("Starting step {StepNumber}: {Type}...", context.StepNumber, _stepType.Name);
            await OnRunAsync(cancellation).ConfigureAwait(false);
            _logger.LogDebug("Running step {StepNumber}: {Type}...", context.StepNumber, _stepType.Name);
            await using var next = _stepFactory.Create<TContext>(NextStep);
            if (next is not null) {
                await next.RunAsync(context, cancellation).ConfigureAwait(false);
                await next.DisposeAsync().ConfigureAwait(false);
            }

            _logger.LogDebug("Finishing step {StepNumber}: {Type}...", context.StepNumber, _stepType.Name);
            await OnFinishAsync(cancellation).ConfigureAwait(false);
            _logger.LogDebug("Step {StepNumber}: {Type} finished.", context.StepNumber, _stepType.Name);
        }
        catch (OperationCanceledException ex) {
            _logger.LogError(ex, "Step {StepNumber}: {Type} cancelled.", context.StepNumber, _stepType.Name);
            if (context.ThrowOnError) throw;
        }
        catch (Exception ex) {
            _logger.LogError(ex, "An error has occurred while executing step {StepNumber}: {Type}.", context.StepNumber, _stepType.Name);
            await OnErrorAsync(ex, cancellation).ConfigureAwait(false);
            if (context.ThrowOnError) throw new ProcedureException($"An error has occurred while executing step {context.StepNumber}: {_stepType.Name}.", ex);
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

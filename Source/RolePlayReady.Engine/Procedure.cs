namespace RolePlayReady.Engine;

public abstract class Procedure<TContext> : IAsyncDisposable
    where TContext : EmptyContext {

    private readonly TContext _context;
    private readonly IStepFactory _stepFactory;
    private readonly ILogger _logger;

    protected Procedure(TContext context, IStepFactory? stepFactory, ILoggerFactory? loggerFactory) {
        _stepFactory = stepFactory ?? new StepFactory();
        _logger = loggerFactory?.CreateLogger(GetType()) ?? NullLoggerFactory.Instance.CreateLogger(GetType());
        _context = Throw.IfNull(context);
        Name = GetType().Name;
    }

    [SetsRequiredMembers]
    protected Procedure(string name, TContext context, IStepFactory? stepFactory, ILoggerFactory? loggerFactory)
        : this(context, stepFactory, loggerFactory) {
        Name = Throw.IfNullOrWhiteSpaces(name);
    }

    public string Name { get; }
    public bool IsRunning { get; private set; }

    protected virtual Task<Type?> OnStartAsync(CancellationToken cancellation = default)
        => Task.FromResult((Type?)null);
    protected virtual Task OnFinishAsync(CancellationToken cancellation = default)
        => Task.CompletedTask;
    protected virtual Task OnErrorAsync(Exception ex, CancellationToken cancellation = default)
        => Task.CompletedTask;

    public async Task<TContext> RunAsync(CancellationToken cancellation = default) {
        try {
            if (IsRunning) return _context;
            IsRunning = true;
            await _context.ResetAsync().ConfigureAwait(false);
            _logger.LogDebug("Starting process {Name}...", Name);
            await OnStartAsync(cancellation).ConfigureAwait(false);
            var firstStepType = await OnStartAsync(cancellation).ConfigureAwait(false);
            if (firstStepType is not null) {
                _logger.LogDebug("Expecting first step to be '{FirstStepType}'.", firstStepType.Name);
                await using var firstStep = _stepFactory.Create<TContext>(firstStepType);
                await firstStep.RunAsync(_context, cancellation).ConfigureAwait(false);
            }

            _logger.LogDebug("Finishing process {Name}...", Name);
            await OnFinishAsync(cancellation).ConfigureAwait(false);
            _logger.LogDebug("Process {Name} finished.", Name);
        }
        catch (OperationCanceledException ex) {
            _logger.LogError(ex, "Process {Name} cancelled.", Name);
            if (_context.ThrowsOnError) throw;
        }
        catch (Exception ex) {
            _logger.LogError(ex, "There was an error while executing procedure {Name}.", Name);
            await OnErrorAsync(ex, cancellation).ConfigureAwait(false);
            if (_context.ThrowsOnError) throw new ProcedureException($"There was an error while executing procedure {Name}.", ex);
        }
        finally {
            IsRunning = false;
        }
        return _context;
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

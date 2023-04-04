namespace RolePlayReady.Engine;

public abstract class Procedure<TContext, TProcedure> : IAsyncDisposable
    where TContext : ProcedureContext<TContext>
    where TProcedure : Procedure<TContext, TProcedure> {

    private readonly TContext _context;
    private readonly Type _initialStep;
    private readonly IStepFactory _stepFactory;
    private readonly ILogger<TProcedure> _logger;

    protected Procedure(TContext context, Type initialStep, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null) {
        _stepFactory = stepFactory ?? new StepFactory();
        _logger = loggerFactory?.CreateLogger<TProcedure>() ?? NullLogger<TProcedure>.Instance;
        _context = Throw.IfNull(context);
        _initialStep = Throw.IfNull(initialStep);
        Name = GetType().Name;
    }

    [SetsRequiredMembers]
    protected Procedure(TContext context, string name, Type initialStep, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : this(context, initialStep, stepFactory, loggerFactory) {
        Name = Throw.IfNullOrWhiteSpaces(name);
    }

    public string Name { get; }

    protected virtual Task OnBeforeStartAsync(CancellationToken cancellation = default) => Task.CompletedTask;
    protected virtual Task OnFinishAsync(CancellationToken cancellation = default) => Task.CompletedTask;
    protected virtual Task OnErrorAsync(Exception ex, CancellationToken cancellation = default) => Task.CompletedTask;

    public async Task<TContext> RunAsync(CancellationToken cancellation = default) {
        try {
            _logger.LogDebug("Starting process {Name}...", Name);
            await OnBeforeStartAsync(cancellation).ConfigureAwait(false);
            _logger.LogDebug("Running process {Name}...", Name);
            await using var start = _stepFactory.Create<TContext>(_initialStep);
            if (start is not null) {
                await start.RunAsync(_context, cancellation).ConfigureAwait(false);
                await start.DisposeAsync().ConfigureAwait(false);
            }

            _logger.LogDebug("Finishing process {Name}...", Name);
            await OnFinishAsync(cancellation).ConfigureAwait(false);
            _logger.LogDebug("Process {Name} finished.", Name);
        }
        catch (OperationCanceledException ex) {
            _logger.LogError(ex, "Process {Name} cancelled.", Name);
            if (_context.ThrowOnError) throw;
        }
        catch (Exception ex) {
            _logger.LogError(ex, "There was an error while executing procedure {Name}.", Name);
            await OnErrorAsync(ex, cancellation).ConfigureAwait(false);
            if (_context.ThrowOnError) throw new ProcedureException($"There was an error while executing procedure {Name}.", ex);
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

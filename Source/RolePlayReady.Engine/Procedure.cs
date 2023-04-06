namespace RolePlayReady.Engine;

public abstract class Procedure<TContext> : IProcedure<TContext>
    where TContext : Context {

    private readonly TContext _context;
    private readonly IStepFactory _stepFactory;
    private readonly ILogger _logger;


    [SetsRequiredMembers]
    protected Procedure(string name, TContext context, IStepFactory stepFactory, ILoggerFactory? loggerFactory)
        : this(context, stepFactory, loggerFactory) {
        Name = Throw.IfNullOrWhiteSpaces(name);
    }

    protected Procedure(TContext context, IStepFactory stepFactory, ILoggerFactory? loggerFactory) {
        _logger = loggerFactory?.CreateLogger(GetType()) ?? NullLoggerFactory.Instance.CreateLogger(GetType());
        _stepFactory = Throw.IfNull(stepFactory);
        _context = Throw.IfNull(context);
        if (_context.IsInProgress)
            throw new ArgumentException("The context is being processed by another job.", nameof(context));
        Name = GetType().Name;
    }

    public string Name { get; }

    protected virtual Task<Type?> OnStartAsync(CancellationToken cancellation = default)
        => Task.FromResult((Type?)null);
    protected virtual Task OnFinishAsync(CancellationToken cancellation = default)
        => Task.CompletedTask;
    protected virtual Task OnErrorAsync(Exception ex, CancellationToken cancellation = default)
        => Task.CompletedTask;

    public async Task<TContext> RunAsync(CancellationToken cancellation = default) {
        try {
            if (_context.IsInProgress)
                throw new InvalidOperationException("The context is being processed by another job.");
            _context.IsInProgress = true;
            await _context.ResetAsync().ConfigureAwait(false);
            _logger.LogDebug("Starting process {Name}...", Name);
            var firstStepType = await OnStartAsync(cancellation).ConfigureAwait(false);
            if (firstStepType is not null) {
                _logger.LogDebug("Expecting first step to be '{FirstStepType}'.", firstStepType.Name);
                await using var firstStep = _stepFactory.Create(firstStepType);
                await firstStep.RunAsync(_context, cancellation).ConfigureAwait(false);
            }

            _logger.LogDebug("Finishing process {Name}...", Name);
            await OnFinishAsync(cancellation).ConfigureAwait(false);
            _logger.LogDebug("Process {Name} finished.", Name);
        }
        catch (OperationCanceledException ex) {
            _logger.LogError(ex, "Process {Name} cancelled.", Name);
            throw;
        }
        catch (Exception ex) {
            _logger.LogError(ex, "There was an error while executing procedure {Name}.", Name);
            await OnErrorAsync(ex, cancellation).ConfigureAwait(false);
            throw new ProcedureException($"There was an error while executing procedure {Name}.", ex);
        }
        finally {
            _context.IsInProgress = false;
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

namespace RolePlayReady.Engine;

public abstract class Procedure<TContext, TProcedure> : IAsyncDisposable
    where TContext : ProcedureContext<TContext>
    where TProcedure : Procedure<TContext, TProcedure> {
    private readonly IStepFactory _stepFactory;
    private readonly TContext _context;
    private readonly Type _initialStep;

    protected Procedure(TContext context, Type initialStep, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null) {
        _stepFactory = stepFactory ?? new StepFactory();
        Logger = loggerFactory?.CreateLogger<TProcedure>() ?? NullLogger<TProcedure>.Instance;
        _context = Throw.IfNull(context);
        _initialStep = Throw.IfNull(initialStep);
        Name = GetType().Name;
    }

    [SetsRequiredMembers]
    protected Procedure(string name, TContext context, Type initialStep, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : this(context, initialStep, stepFactory, loggerFactory) {
        Name = Throw.IfNullOrWhiteSpaces(name);
    }

    public string Name { get; }

    protected ILogger<TProcedure> Logger { get; }
    protected virtual Task OnBeforeStartAsync(CancellationToken cancellation) => Task.CompletedTask;
    protected virtual Task OnFinishAsync(CancellationToken cancellation) => Task.CompletedTask;
    protected virtual Task OnErrorAsync(Exception ex, CancellationToken cancellation) => Task.CompletedTask;

    public async Task<TContext> RunAsync(CancellationToken cancellation) {
        try {
            Logger.LogDebug("Starting process {Name}...", Name);
            await OnBeforeStartAsync(cancellation);
            Logger.LogDebug("Running process {Name}...", Name);
            await using var start = _stepFactory.Create<TContext>(_initialStep);
            await start.RunAsync(_context, cancellation);
            await start.DisposeAsync();
            if (cancellation.IsCancellationRequested) {
                Logger.LogDebug("Process {Name} cancelled.", Name);
                return _context;
            }

            Logger.LogDebug("Finishing process {Name}...", Name);
            await OnFinishAsync(cancellation);
            Logger.LogDebug("Process {Name} finished.", Name);
        }
        catch (Exception ex) {
            Logger.LogError(ex, "There was an error while executing procedure {Name}.", Name);
            await OnErrorAsync(ex, cancellation);
            if (_context.ThrowOnError)
                throw new ProcedureException($"There was an error while executing procedure {Name}.", ex);
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
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}

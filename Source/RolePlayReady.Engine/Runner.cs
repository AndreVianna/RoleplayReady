namespace RolePlayReady.Engine;

public abstract class Runner<TContext, TOptions> : IRunner<TContext, TOptions>
    where TContext : class, IContext
    where TOptions : class, IRunnerOptions<TOptions> {

    private readonly IStepFactory _stepFactory;
    private readonly ILogger _logger;


    [SetsRequiredMembers]
    protected Runner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory) {
        _logger = loggerFactory?.CreateLogger(GetType()) ?? NullLoggerFactory.Instance.CreateLogger(GetType());

        var options = Activator.CreateInstance<TOptions>();
        configuration.GetSection(typeof(TOptions).Name).Bind(options);
        _stepFactory = Throw.IfNull(stepFactory);
        Options = options;
        if (string.IsNullOrWhiteSpace(Options.Name)) Options.Name = GetType().Name;
    }

    public TOptions Options { get; }

    protected virtual Task<Type?> OnStartAsync(TContext context, CancellationToken cancellation = default)
        => Task.FromResult<Type?>(default);
    protected virtual Task OnFinishAsync(TContext context, CancellationToken cancellation = default)
        => Task.CompletedTask;
    protected virtual Task OnErrorAsync(Exception ex, TContext context, CancellationToken cancellation = default)
        => Task.CompletedTask;

    async Task<IContext> IIsRunnable.RunAsync(IContext context, CancellationToken cancellation)
        => await RunAsync(
            Throw.IfNull(context as TContext, $"Context must be of type '{typeof(TContext).Name}'."),
            cancellation);
    public async Task<TContext> RunAsync(TContext context, CancellationToken cancellation = default) {
        try {
            if (context.IsBlocked)
                throw new InvalidOperationException("The context is being processed by another process.");
            context.Block();
            await context.InitializeAsync(cancellation).ConfigureAwait(false);
            _logger.LogDebug("Starting process {Name}...", Options.Name);
            var firstStepType = await OnStartAsync(context, cancellation).ConfigureAwait(false);
            if (firstStepType is not null) {
                _logger.LogDebug("Expecting first step to be '{FirstStepType}'.", firstStepType.Name);
                await using var firstStep = _stepFactory.Create(firstStepType);
                await firstStep.RunAsync(context, cancellation).ConfigureAwait(false);
            }

            _logger.LogDebug("Finishing process {Name}...", Options.Name);
            await OnFinishAsync(context, cancellation).ConfigureAwait(false);
            _logger.LogDebug("Process {Name} finished.", Options.Name);
        }
        catch (OperationCanceledException ex) {
            _logger.LogError(ex, "Process {Name} cancelled.", Options.Name);
            throw;
        }
        catch (Exception ex) {
            _logger.LogError(ex, "There was an error while executing procedure {Name}.", Options.Name);
            await OnErrorAsync(ex, context, cancellation).ConfigureAwait(false);
            throw new ProcedureException($"There was an error while executing procedure {Options.Name}.", ex);
        }
        finally {
            context.Release();
        }

        return context;
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

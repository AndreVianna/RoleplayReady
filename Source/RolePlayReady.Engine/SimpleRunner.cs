namespace RolePlayReady.Engine;

public abstract class SimpleRunner<TContext, TOptions> : IRunner<TContext, TOptions>
    where TContext : class, IContext
    where TOptions : class, IRunnerOptions<TOptions> {
    private readonly IStepFactory _stepFactory;
    private readonly ILogger _logger;

    [SetsRequiredMembers]
    protected SimpleRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory) {
        _logger = loggerFactory?.CreateLogger(GetType()) ?? NullLoggerFactory.Instance.CreateLogger(GetType());

        var options = Activator.CreateInstance<TOptions>();
        configuration.GetSection(options.Name).Bind(options);
        _stepFactory = Ensure.IsNotNull(stepFactory);
        Options = options;
    }

    public TOptions Options { get; }

    protected virtual Task<TContext> OnRunAsync(TContext context, CancellationToken cancellation = default)
        => Task.FromResult(context);
    protected virtual Task OnErrorAsync(Exception ex, TContext context, CancellationToken cancellation = default)
        => Task.CompletedTask;

    public async Task<TContext> RunAsync(TContext context, CancellationToken cancellation = default) {
        try {
            if (context.IsBlocked)
                throw new InvalidOperationException("The context is being processed by another process.");
            context.Block();
            await context.InitializeAsync(cancellation).ConfigureAwait(false);
            _logger.LogDebug("Running process {Name}...", Options.Name);
            context = await OnRunAsync(context, cancellation).ConfigureAwait(false);
            _logger.LogDebug("Process {Name} finished.", Options.Name);

            return context;
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
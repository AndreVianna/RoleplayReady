namespace RolePlayReady.Engine;

public abstract class Context<TContext> : IContext
    where TContext : Context<TContext> {
    protected Context(IServiceProvider services) {
        Services = services;
    }

    public IServiceProvider Services { get; }

    public bool IsInProgress { get; set; }
    public int CurrentStepNumber { get; set; }
    public Type CurrentStepType { get; set; } = typeof(EndStep<TContext>);

    public virtual Task ResetAsync() {
        CurrentStepNumber = 0;
        CurrentStepType = typeof(EndStep<TContext>);
        return Task.CompletedTask;
    }

    private bool _disposed;
    protected virtual ValueTask DisposeAsync(bool disposing) {
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
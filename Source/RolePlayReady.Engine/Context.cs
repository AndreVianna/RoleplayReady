namespace RolePlayReady.Engine;

public abstract class Context : IContext {
    protected Context(IServiceProvider services) {
        Services = services;
    }

    public IServiceProvider Services { get; }

    public bool IsInProgress { get; internal set; }
    public int CurrentStepNumber { get; internal set; }
    public Type CurrentStepType { get; internal set; } = default!;

    public virtual Task ResetAsync() {
        CurrentStepNumber = 0;
        return Task.CompletedTask;
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
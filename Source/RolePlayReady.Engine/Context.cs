namespace RolePlayReady.Engine;

public interface IContext : IAsyncDisposable {
    int CurrentStepNumber { get; }
    Type CurrentStepType { get; }

    IServiceProvider? Services { get; }
}

public abstract class Context : IContext {
    private static readonly IServiceCollection _emptyServiceCollection = new ServiceCollection();
    private static readonly IServiceProvider _emptyServiceProvider = _emptyServiceCollection.BuildServiceProvider();

    protected Context(IServiceCollection? services = null) {
        Services = services?.BuildServiceProvider() ?? _emptyServiceProvider;
    }

    public IServiceProvider? Services { get; }

    public bool IsInProgress { get; internal set; }
    public int CurrentStepNumber { get; internal set; }
    public Type CurrentStepType { get; internal set; } = default!;

    internal Task ResetAsync() => OnResetAsync();

    protected virtual Task OnResetAsync() {
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
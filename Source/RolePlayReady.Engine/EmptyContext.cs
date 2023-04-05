namespace RolePlayReady.Engine;

public class EmptyContext : IAsyncDisposable {
    public static EmptyContext Silent { get; } = new(false);
    public static EmptyContext Default { get; } = new();

    private static readonly ServiceProvider _emptyProvider = (new ServiceCollection()).BuildServiceProvider();

    public EmptyContext(IServiceProvider? serviceProvider = null)
        : this(true, serviceProvider) {
    }

    public EmptyContext(bool throwsOnError, IServiceProvider? serviceProvider = null) {
        ThrowsOnError = throwsOnError;
        Services = serviceProvider ?? _emptyProvider;
    }

    public bool ThrowsOnError { get; }
    public IServiceProvider? Services { get; } 

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

using RolePlayReady.Engine.Abstractions;
using RolePlayReady.Engine.Steps.Abstractions;

namespace RolePlayReady.Engine;

public class Context : IContext {
    public Context(IServiceProvider services) {
        Services = services;
    }

    public IServiceProvider Services { get; }

    public bool IsBlocked { get; private set; }
    public int CurrentStepNumber { get; private set; }
    public IStep? CurrentStep { get; private set; }

    public void Block() => IsBlocked = true;

    public virtual Task InitializeAsync(CancellationToken cancellationToken = default) {
        CurrentStepNumber = 0;
        CurrentStep = default;
        return Task.CompletedTask;
    }

    public virtual Task UpdateAsync(IStep currentStepType, CancellationToken cancellationToken = default) {
        CurrentStepNumber++;
        CurrentStep = currentStepType;
        return Task.CompletedTask;
    }

    public void Release() => IsBlocked = false;

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

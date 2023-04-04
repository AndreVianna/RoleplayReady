namespace RolePlayReady.Engine;

public abstract class ProcedureContext<TContext> : IAsyncDisposable
    where TContext : ProcedureContext<TContext> {
    protected ProcedureContext() {
        ThrowOnError = true;
    }

    [SetsRequiredMembers]
    protected ProcedureContext(bool throwOnError = true) {
        ThrowOnError = throwOnError;
    }

    public bool ThrowOnError { get; init; }

    public int StepNumber { get; private set; }
    public void IncrementStepNumber() => StepNumber++;

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

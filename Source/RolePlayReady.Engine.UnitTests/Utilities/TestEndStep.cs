namespace RolePlayReady.Engine.Utilities;

internal class TestEndStep : EndStep {
    public Task<Type?> TestOnRunAsync(CancellationToken cancellation = default)
        => OnRunAsync(cancellation);

    public Task TestOnErrorAsync(Exception ex, CancellationToken cancellation = default)
        => OnErrorAsync(ex, cancellation);

    public Task TestOnFinishAsync(CancellationToken cancellation = default)
        => OnFinishAsync(cancellation);
}
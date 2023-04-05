namespace RolePlayReady.Engine.Nulls;

public sealed class NullStep : IStep {
    private NullStep() { }

    public static NullStep Instance { get; } = new();

    public Task RunAsync(IContext _, CancellationToken __ = default) => Task.CompletedTask;

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
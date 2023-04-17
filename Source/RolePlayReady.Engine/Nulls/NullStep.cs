namespace RolePlayReady.Engine.Nulls;

public sealed class NullStep : IStep {
    private NullStep() { }

    public static NullStep Instance { get; } = new();

    public Task<IContext> RunAsync(IContext context, CancellationToken cancellation = default) => Task.FromResult(context);

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
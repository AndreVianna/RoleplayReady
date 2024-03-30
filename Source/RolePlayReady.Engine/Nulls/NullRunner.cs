namespace RolePlayReady.Engine.Nulls;

public sealed class NullRunner : IRunner<IContext, RunnerOptions> {
    private NullRunner() { }

    public static NullRunner Instance { get; } = new();

    public string Name => nameof(NullRunner);

    public RunnerOptions Options => new();

    public Task<IContext> RunAsync(IContext context, CancellationToken cancellation = default) => Task.FromResult(context);

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
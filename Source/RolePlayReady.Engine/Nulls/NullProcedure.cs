namespace RolePlayReady.Engine.Nulls;

public sealed class NullProcedure : IProcedure {
    private NullProcedure() { }

    public static NullProcedure Instance { get; } = new();

    public string Name => nameof(NullProcedure);
    public Task<IContext> RunAsync(CancellationToken _ = default) => Task.FromResult<IContext>(NullContext.Instance);
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
namespace RolePlayReady.Engine.Nulls;

public sealed class NullContext : IContext {
    private NullContext() { }
    public static NullContext Instance { get; } = new();

    public IServiceProvider Services => NullServiceProvider.Instance;

    public bool IsInProgress => false;
    public int CurrentStepNumber => 1;
    public Type CurrentStepType => typeof(NullStep);

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}

namespace RolePlayReady.Engine.Nulls;

public sealed class NullContext : IContext {
    public static NullContext Instance { get; } = new();

    public IServiceProvider Services => NullServiceProvider.Instance;

    public bool IsInProgress { get => false; set { } }
    public int CurrentStepNumber { get => 0; set { } }
    public Type CurrentStepType { get => typeof(NullStep); set { } }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    public Task ResetAsync() => Task.CompletedTask;
}

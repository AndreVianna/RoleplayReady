using RolePlayReady.Engine.Contracts;

namespace RolePlayReady.Engine.Nulls;

public sealed class NullContext : IContext {
    public static NullContext Instance { get; } = new();

    public IServiceProvider Services => NullServiceProvider.Instance;

    public bool IsBlocked => false;
    public int CurrentStepNumber => 0;
    public IStep? CurrentStep => default;

    public void Block() { }
    public Task InitializeAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    public Task UpdateAsync(IStep currentStep, CancellationToken cancellationToken = default) => Task.CompletedTask;
    public void Release() { }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}

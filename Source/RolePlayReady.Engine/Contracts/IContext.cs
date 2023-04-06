namespace RolePlayReady.Engine.Contracts;

public interface IContext : IAsyncDisposable {
    IServiceProvider Services { get; }

    bool IsBlocked { get; }
    int CurrentStepNumber { get; }
    IStep? CurrentStep { get; }

    void Block();
    Task InitializeAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(IStep currentStep, CancellationToken cancellationToken = default);
    void Release();
}
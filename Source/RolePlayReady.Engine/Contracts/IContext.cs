namespace RolePlayReady.Engine.Contracts;

public interface IContext : IAsyncDisposable {
    IServiceProvider Services { get; }
    bool IsInProgress { get; }

    int CurrentStepNumber { get; }
    Type CurrentStepType { get; }

}
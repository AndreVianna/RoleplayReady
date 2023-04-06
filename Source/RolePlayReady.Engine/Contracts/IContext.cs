namespace RolePlayReady.Engine.Contracts;

public interface IContext : IAsyncDisposable {
    IServiceProvider Services { get; }

    bool IsInProgress { get; set; }
    int CurrentStepNumber { get; set; }
    Type CurrentStepType { get; set; }

    Task ResetAsync();
}
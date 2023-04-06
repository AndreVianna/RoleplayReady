namespace RolePlayReady.Engine.Contracts;

public interface IStep : IAsyncDisposable {
    Task RunAsync(Context context, CancellationToken cancellation = default);
}

namespace RolePlayReady.Engine;

public interface IStep : IAsyncDisposable {
    Task RunAsync(Context context, CancellationToken cancellation = default);
}

public interface IStep<in TContext> : IStep
    where TContext : Context {
    Task RunAsync(TContext context, CancellationToken cancellation = default);
}

namespace RolePlayReady.Engine.Contracts;

public interface IIsRunnable<TContext> : IIsRunnable
    where TContext : class, IContext {
    Task<TContext> RunAsync(TContext context, CancellationToken cancellation = default);
}

public interface IIsRunnable : IAsyncDisposable {
    Task<IContext> RunAsync(IContext context, CancellationToken cancellation = default);
}
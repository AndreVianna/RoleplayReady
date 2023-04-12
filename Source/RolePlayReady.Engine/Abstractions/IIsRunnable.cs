namespace RolePlayReady.Engine.Abstractions;

public interface IIsRunnable<TContext, TResult> : IIsRunnable
    where TContext : class, IContext {
    Task<TResult> RunAsync(TContext context, Func<TContext, TResult> resultSelector, CancellationToken cancellation = default);
}

public interface IIsRunnable<TContext> : IIsRunnable
    where TContext : class, IContext {
    Task<TContext> RunAsync(TContext context, CancellationToken cancellation = default);
}

public interface IIsRunnable : IAsyncDisposable {
    Task<IContext> RunAsync(IContext context, CancellationToken cancellation = default);
}
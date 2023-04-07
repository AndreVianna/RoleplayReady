namespace RolePlayReady.Engine.Contracts;

public interface IStep : IAsyncDisposable {
    Task<IContext> RunAsync(IContext context, CancellationToken cancellation = default);
}

public interface IStep<TContext> : IStep
    where TContext : class, IContext {
    Task<TContext> RunAsync(TContext context, CancellationToken cancellation = default);
}
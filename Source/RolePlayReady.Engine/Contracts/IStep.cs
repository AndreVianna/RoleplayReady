namespace RolePlayReady.Engine.Contracts;

public interface IStep : IAsyncDisposable {
    Task RunAsync(IContext context, CancellationToken cancellation = default);
}

public interface IStep<in TContext> : IStep
    where TContext : IContext {
    Task RunAsync(TContext context, CancellationToken cancellation = default);
}

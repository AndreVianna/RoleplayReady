namespace RolePlayReady.Engine.Contracts;

public interface IProcedure<TContext> : IAsyncDisposable
    where TContext : IContext {
    string Name { get; }
    Task<TContext> RunAsync(CancellationToken cancellation = default);
}
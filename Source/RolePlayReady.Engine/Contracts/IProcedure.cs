namespace RolePlayReady.Engine.Contracts;

public interface IProcedure : IAsyncDisposable {
    string Name { get; }
    Task<IContext> RunAsync(CancellationToken cancellation = default);
}

public interface IProcedure<TContext> : IProcedure
    where TContext : IContext {
    new Task<TContext> RunAsync(CancellationToken cancellation = default);
}
namespace RolePlayReady.Engine.Contracts;

public interface IRunner : IAsyncDisposable {
}

public interface IRunner<TContext, out TOptions> : IRunner
    where TContext : class, IContext
    where TOptions : class, IRunnerOptions<TOptions> {

    public TOptions Options { get; }
    Task<TContext> RunAsync(TContext context, CancellationToken cancellation = default);
}

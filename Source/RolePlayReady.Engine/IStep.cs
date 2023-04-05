namespace RolePlayReady.Engine;

public interface IStep {
    Task RunAsync(EmptyContext context, CancellationToken cancellation = default);
    ValueTask DisposeAsync();
}

public interface IStep<in TContext> : IStep
    where TContext : EmptyContext {
    Task RunAsync(TContext context, CancellationToken cancellation = default);
}

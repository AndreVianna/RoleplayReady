namespace RolePlayReady.Engine.Contracts;

public interface IRunner : IIsRunnable {
}

public interface IRunner<TContext, out TOptions> : IIsRunnable<TContext>, IRunner
    where TContext : class, IContext
    where TOptions : class, IRunnerOptions<TOptions> {

    public TOptions Options { get; }
}

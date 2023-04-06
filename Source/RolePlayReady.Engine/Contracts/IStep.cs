namespace RolePlayReady.Engine.Contracts;

public interface IStep : IIsRunnable {
}

public interface IStep<TContext> : IIsRunnable<TContext>, IStep
    where TContext : class, IContext {
}
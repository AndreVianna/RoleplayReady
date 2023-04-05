namespace RolePlayReady.Engine;

public interface IStepFactory {
    Step<TContext> Create<TContext>(Type stepType)
        where TContext : EmptyContext;
}

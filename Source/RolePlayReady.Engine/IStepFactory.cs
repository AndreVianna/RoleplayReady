namespace RolePlayReady.Engine;

public interface IStepFactory {
    ProcedureStep<TContext>? Create<TContext>(Type? step)
        where TContext : ProcedureContext<TContext>;
}

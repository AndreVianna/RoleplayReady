namespace RolePlayReady.Engine;

public class StepFactory : IStepFactory {
    private readonly ILoggerFactory _loggerFactory;

    public StepFactory(ILoggerFactory? loggerFactory = null) {
        _loggerFactory = loggerFactory ?? new NullLoggerFactory();
    }

    public ProcedureStep<TContext> Create<TContext>(Type step)
        where TContext : ProcedureContext<TContext>
        => (ProcedureStep<TContext>)Activator.CreateInstance(step, this, _loggerFactory)!;
}

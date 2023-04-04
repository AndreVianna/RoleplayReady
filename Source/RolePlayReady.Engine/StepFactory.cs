namespace RolePlayReady.Engine;

public class StepFactory : IStepFactory {
    private readonly ILoggerFactory _loggerFactory;

    public StepFactory(ILoggerFactory? loggerFactory = null) {
        _loggerFactory = loggerFactory ?? new NullLoggerFactory();
    }

    public ProcedureStep<TContext>? Create<TContext>(Type? step)
        where TContext : ProcedureContext<TContext>
        => step is null
            ? default
            :  step.IsAssignableTo(typeof(ProcedureStep<TContext>))
                ? Activator.CreateInstance(step, this, _loggerFactory) as ProcedureStep<TContext>
                : throw new InvalidCastException($"Step type must be assignable to ProcedureStep<{typeof(TContext).Name}>.");
}

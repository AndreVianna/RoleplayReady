namespace RolePlayReady.Engine;

public class StepFactory : IStepFactory {
    private readonly IServiceProvider? _serviceProvider;
    private readonly ILoggerFactory _loggerFactory;

    public StepFactory(IServiceProvider? serviceProvider = null, ILoggerFactory? loggerFactory = null) {
        _serviceProvider = serviceProvider;
        _loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
    }

    public virtual Step<TContext> Create<TContext>(Type stepType)
        where TContext : EmptyContext =>
        Throw.IfNull(stepType).IsAssignableTo(typeof(Step<TContext>))
            ? _serviceProvider?.GetService(stepType) as Step<TContext>
              ?? (Step<TContext>)Activator.CreateInstance(stepType, this, _loggerFactory)!
            : throw new InvalidOperationException($"Could not find a valid step of type '{stepType.Name}'.");
}

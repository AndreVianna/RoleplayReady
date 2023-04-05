namespace RolePlayReady.Engine;

public class StepFactory : IStepFactory {
    private readonly IServiceProvider? _serviceProvider;
    private readonly ILoggerFactory _loggerFactory;

    public StepFactory(IServiceCollection? services = null, ILoggerFactory? loggerFactory = null) {
        _serviceProvider = services?.BuildServiceProvider();
        _loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
    }

    public virtual IStep Create(Type stepType)
        => Throw.IfNull(stepType).IsAssignableTo(typeof(IStep))
            ? _serviceProvider?.GetService(stepType) as IStep
              ?? (IStep)Activator.CreateInstance(stepType, this, _loggerFactory)!
            : throw new InvalidOperationException($"Could not find a valid step of type '{stepType.Name}'.");
}

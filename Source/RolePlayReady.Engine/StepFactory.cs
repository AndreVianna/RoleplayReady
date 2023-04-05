namespace RolePlayReady.Engine;

public class StepFactory : IStepFactory {
    private readonly IServiceProvider? _serviceProvider;
    private readonly ILoggerFactory _loggerFactory;

    public StepFactory(IServiceCollection? services = null, ILoggerFactory? loggerFactory = null) {
        _serviceProvider = services?.BuildServiceProvider();
        _loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
    }

    public virtual IStep Create(Type stepType)
        => !Throw.IfNull(stepType).IsAssignableTo(typeof(IStep))
            ? throw new InvalidOperationException($"'{stepType.Name}' must be derived from '{nameof(IStep)}'.")
            : (IStep)(_serviceProvider?.GetService(stepType)
                      ?? Activator.CreateInstance(stepType, this, _loggerFactory)!);
}

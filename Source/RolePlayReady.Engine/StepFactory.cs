namespace RolePlayReady.Engine;

public class StepFactory : IStepFactory {
    private readonly IServiceProvider _services;

    public StepFactory(IServiceProvider services) {
        _services = Throw.IfNull(services);
    }

    public virtual IStep Create(Type stepType)
        => _services.GetService(Throw.IfNull(stepType)) as IStep
            ?? throw new InvalidOperationException($"Step '{stepType.Name}' not found.");
}

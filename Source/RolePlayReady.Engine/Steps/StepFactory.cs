using RolePlayReady.Engine.Steps.Abstractions;

namespace RolePlayReady.Engine.Steps;

public class StepFactory : IStepFactory {
    private readonly IServiceProvider _services;

    public StepFactory(IServiceProvider services) {
        _services = Ensure.NotNull(services);
    }

    public virtual IStep Create(Type stepType)
        => _services.GetService(Ensure.NotNull(stepType)) as IStep
            ?? throw new InvalidOperationException($"Step '{stepType.Name}' not found.");
}

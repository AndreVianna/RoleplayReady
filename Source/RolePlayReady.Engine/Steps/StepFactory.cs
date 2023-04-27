using System.Utilities;

namespace RolePlayReady.Engine.Steps;

public class StepFactory : IStepFactory {
    private readonly IServiceProvider _services;

    public StepFactory(IServiceProvider services) {
        _services = Ensure.IsNotNull(services);
    }

    public virtual IStep Create(Type stepType)
        => _services.GetService(Ensure.IsNotNull(stepType)) as IStep
            ?? throw new InvalidOperationException($"Step '{stepType.Name}' not found.");
}

namespace RolePlayReady.Engine.Factories;

public class StepFactory : IStepFactory {
    private readonly IServiceCollection _services;

    public StepFactory(IServiceCollection services) {
        _services = Throw.IfNull(services);
    }

    public virtual IStep Create(Type stepType)
        => Throw.IfNull(stepType).IsAssignableTo(typeof(IStep))
            ? (IStep)_services.BuildServiceProvider().GetRequiredService(stepType)
            : throw new InvalidOperationException($"'{stepType.Name}' must be derived from '{nameof(IStep)}'.");
}

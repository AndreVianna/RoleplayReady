namespace RolePlayReady.Engine.Utilities;

internal class FirstStep : Step {
    public FirstStep(IServiceCollection services) : base(services) { }

    protected override Task<Type?> OnRunAsync(CancellationToken cancellation = default)
        => Task.FromResult((Type?)typeof(DefaultEndStep));
}
namespace RolePlayReady.Engine.Utilities;

internal class FaultyStep : Step {
    public FaultyStep(IServiceCollection services) : base(services) { }


    protected override Task<Type?> OnRunAsync(CancellationToken cancellation = default)
        => throw new Exception("Some exception.");
}
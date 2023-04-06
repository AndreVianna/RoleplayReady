namespace RolePlayReady.Engine.Utilities;

internal class TestStep : Step {
    public TestStep(IStepFactory stepFactory) : base(stepFactory) { }

    protected override Task<Type?> OnRunAsync(CancellationToken cancellation = default)
        => Task.FromResult((Type?)typeof(EndStep));
}
namespace RolePlayReady.Engine.Utilities;

internal class TestStep<TContext> : Step<TContext>
    where TContext : class, IContext {
    public TestStep(IStepFactory stepFactory) : base(stepFactory) { }

    protected override Task<Type?> OnRunAsync(TContext context, CancellationToken cancellation = default)
        => Task.FromResult((Type?)typeof(EndStep<TContext>));
}
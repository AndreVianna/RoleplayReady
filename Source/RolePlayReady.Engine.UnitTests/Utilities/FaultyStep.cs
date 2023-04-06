namespace RolePlayReady.Engine.Utilities;

internal class FaultyStep<TContext> : Step<TContext>
    where TContext : class, IContext {
    public FaultyStep(IStepFactory stepFactory) : base(stepFactory) { }


    protected override Task<Type?> OnRunAsync(TContext context, CancellationToken cancellation = default)
        => throw new Exception("Some exception.");
}
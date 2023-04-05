namespace RolePlayReady.Engine;

public sealed class NullStepFactory : StepFactory {
    private NullStepFactory() {}

    public static NullStepFactory Instance { get; } = new();

    // this method should be unreachable;
    public override Step<TContext> Create<TContext>(Type _) => new EndStep<TContext>();
}
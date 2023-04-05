namespace RolePlayReady.Engine;

public sealed class NullStepFactory : StepFactory {
    private NullStepFactory() { }

    public static NullStepFactory Instance { get; } = new();

    // this method should be unreachable;
    public override IStep Create(Type _) => new EndStep();
}
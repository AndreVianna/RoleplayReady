namespace RolePlayReady.Engine.Nulls;

public sealed class NullStepFactory : IStepFactory {
    private NullStepFactory() { }

    public static NullStepFactory Instance { get; } = new();

    public IStep Create(Type _) => NullStep.Instance;
}
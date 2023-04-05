namespace RolePlayReady.Engine;

public sealed class NullStepFactory : IStepFactory {
    private NullStepFactory() { }

    public static NullStepFactory Instance { get; } = new();

    public IStep Create(Type _) => new EndStep();
}
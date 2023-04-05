namespace RolePlayReady.Engine;

public sealed class NullProcedure : DefaultProcedure {
    private NullProcedure() : base(NullStepFactory.Instance) { }
    public static NullProcedure Instance { get; } = new();
}
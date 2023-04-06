namespace RolePlayReady.Engine.Nulls;

public sealed class NullContext : Context {
    private NullContext() : base(NullServiceProvider.Instance) { }
    public static Context Instance { get; } = new NullContext();
}

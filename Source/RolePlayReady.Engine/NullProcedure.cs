namespace RolePlayReady.Engine;

public sealed class NullProcedure : DefaultProcedure {

    public NullProcedure() : base(new EmptyContext()) { }
}
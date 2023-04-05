namespace RolePlayReady.Engine.Utilities;


internal class FaultyProcedure : DefaultProcedure {
    protected override Task<Type?> OnStartAsync(CancellationToken cancellation = default)
        => throw new Exception("Some exception.");
}
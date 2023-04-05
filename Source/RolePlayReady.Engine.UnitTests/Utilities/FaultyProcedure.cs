namespace RolePlayReady.Engine.Utilities;


internal class FaultyProcedure : DefaultProcedure {
    public FaultyProcedure(IServiceCollection services) : base(services) {
    }

    protected override Task<Type?> OnStartAsync(CancellationToken cancellation = default)
        => throw new Exception("Some exception.");
}
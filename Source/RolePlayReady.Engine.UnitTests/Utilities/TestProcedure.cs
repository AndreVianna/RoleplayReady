namespace RolePlayReady.Engine.Utilities;

internal class TestProcedure : Procedure<DefaultContext> {
    [SetsRequiredMembers]
    public TestProcedure(string name, DefaultContext context, IServiceCollection services)
        : base(name, context, services, null) {
        services.AddStep<FirstStep>();
    }

    protected override Task<Type?> OnStartAsync(CancellationToken cancellation = default)
        => Task.FromResult(typeof(FirstStep))!;
}
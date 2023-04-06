namespace RolePlayReady.Engine.Utilities;

internal class TestProcedure : Procedure<DefaultContext> {
    [SetsRequiredMembers]
    public TestProcedure(string name, DefaultContext context, IStepFactory stepFactory)
        : base(name, context, stepFactory, null) {
    }

    protected override Task<Type?> OnStartAsync(CancellationToken cancellation = default)
        => Task.FromResult(typeof(TestStep<DefaultContext>))!;
}
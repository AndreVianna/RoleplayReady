namespace RolePlayReady.Engine.Utilities;

internal class TestContext : ProcedureContext<TestContext> {
    public TestContext() { }

    [SetsRequiredMembers]
    public TestContext(bool throwOnError = true) : base(throwOnError) { }
}
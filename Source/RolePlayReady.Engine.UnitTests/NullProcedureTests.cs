namespace RolePlayReady.Engine;

public class NullProcedureTests {
    [Fact]
    public async Task RunAsync_ShouldNotThrowException() {
        var nullProcedure = NullProcedure.Instance;

        await nullProcedure.RunAsync();
    }
}
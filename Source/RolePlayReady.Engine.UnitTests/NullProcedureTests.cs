namespace RolePlayReady.Engine;

public class NullProcedureTests {
    [Fact]
    public async Task RunAsync_ShouldNotThrowException() {
        var nullProcedure = new NullProcedure();

        await nullProcedure.RunAsync();
    }
}
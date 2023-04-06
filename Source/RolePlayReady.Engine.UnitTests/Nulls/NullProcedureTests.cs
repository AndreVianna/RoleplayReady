namespace RolePlayReady.Engine.Nulls;

public class NullProcedureTests {
    [Fact]
    public async Task Instance_ReturnsNullContext() {
        await using var nullProcedure = NullProcedure.Instance;

        await nullProcedure.RunAsync();

        nullProcedure.Name.Should().Be(nameof(NullProcedure));
    }
}
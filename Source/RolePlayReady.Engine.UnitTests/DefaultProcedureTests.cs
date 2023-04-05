namespace RolePlayReady.Engine;

public class DefaultProcedureTests {
    private readonly ServiceCollection _services;

    public DefaultProcedureTests() {
        _services = new();
        _services.AddEngine();
    }

    [Fact]
    public void Constructor_WithName_CreatesDefaultProcedure() {

        var procedure = new DefaultProcedure("TestProcedure", _services, NullLoggerFactory.Instance);

        procedure.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithStepFactoryAndLoggerFactory_CreatesDefaultProcedure() {
        var procedure = new DefaultProcedure(_services, NullLoggerFactory.Instance);

        procedure.Should().NotBeNull();
    }
}
namespace RolePlayReady.Extensions;

public class ServiceCollectionExtensionsTests {
    [Fact]
    public void AddDomainHandlers_RegisterHandlers() {
        var services = new ServiceCollection();

        var result = services.AddDomainHandlers();

        result.Should().BeSameAs(services);
    }
}
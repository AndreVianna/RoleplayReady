namespace RolePlayReady.Models;

public class EntityTests {
    private record TestEntity : Entity;

    [Fact]
    public void Constructor_CreatesInstance() {
        var testEntity = new TestEntity {
            Name = "TestName",
            Description = "TestDescription",
        };

        testEntity.Attributes.Should().BeEmpty();
    }
}
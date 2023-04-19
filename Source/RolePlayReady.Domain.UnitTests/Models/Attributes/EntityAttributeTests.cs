namespace RolePlayReady.Models.Attributes;

public class EntityAttributeTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeDefinition<int> {
            Name = "TestName",
            Description = "TestDescription",
        };

        IEntityAttribute subject = new EntityNumberAttribute<int> {
            Attribute = attribute,
            Value = 42
        };

        subject.Attribute.Should().Be(attribute);
        subject.Value.Should().Be(42);
    }
}
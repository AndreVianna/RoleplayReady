namespace RolePlayReady.Models.Attributes;

public class EntityAttributeTests {
    [Fact]
    public void Constructor_InitializesProperties() {
        var attribute = new AttributeDefinition {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(int),
        };

        IEntityAttribute subject = new EntityNumberAttribute<int> {
            Attribute = attribute,
            Value = 42
        };

        subject.Attribute.Should().Be(attribute);
        subject.Value.Should().Be(42);
    }
}
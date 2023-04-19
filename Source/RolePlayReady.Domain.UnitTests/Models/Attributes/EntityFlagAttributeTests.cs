namespace RolePlayReady.Models.Attributes;

public class EntityFlagAttributeTests {
    private readonly AttributeDefinition<bool> _definition;
    private readonly EntityFlagAttribute _attribute;

    public EntityFlagAttributeTests() {
        _definition = new AttributeDefinition<bool> {
            Name = "TestName",
            Description = "TestDescription",
        };

        _attribute = new EntityFlagAttribute {
            Attribute = _definition,
            Value = true
        };
    }

    [Fact]
    public void Constructor_InitializesProperties() {
        _attribute.Attribute.Should().Be(_definition);
        _attribute.Value.Should().Be(true);
        _attribute.IsValid.Should().BeTrue();
    }
}
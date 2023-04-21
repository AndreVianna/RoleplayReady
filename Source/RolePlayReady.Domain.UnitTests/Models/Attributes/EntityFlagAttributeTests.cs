namespace RolePlayReady.Models.Attributes;

public class EntityFlagAttributeTests {
    private readonly AttributeDefinition _definition;
    private readonly EntityFlagAttribute _attribute;

    public EntityFlagAttributeTests() {
        _definition = new() {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(bool),
        };

        _attribute = new() {
            Attribute = _definition,
            Value = true
        };
    }

    [Fact]
    public void Constructor_InitializesProperties() {
        _attribute.Attribute.Should().Be(_definition);
        _attribute.Value.Should().Be(true);
        _attribute.Validate().IsSuccess.Should().BeTrue();
    }
}
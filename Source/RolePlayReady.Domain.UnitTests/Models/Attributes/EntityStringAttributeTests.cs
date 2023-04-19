namespace RolePlayReady.Models.Attributes;

public class EntityStringAttributeTests {
    private readonly AttributeDefinition<string> _definition;
    private readonly EntityStringAttribute _attribute;

    public EntityStringAttributeTests() {
        _definition = new AttributeDefinition<string> {
            Name = "TestName",
            Description = "TestDescription",
        };

        _attribute = new EntityStringAttribute {
            Attribute = _definition,
            Value = "TestValue"
        };
    }

    [Fact]
    public void Constructor_InitializesProperties() {
        _attribute.Attribute.Should().Be(_definition);
        _attribute.Value.Should().Be("TestValue");
        _attribute.IsValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_WithValidConstraint_ReturnsTrue() {
        _definition.Constraints.Add(new AttributeConstraint("LengthIs", "TestValue".Length));

        var result = _attribute.IsValid;

        result.Should().BeTrue();
    }

    [Fact]
    public void IsValid_FailedConstraint_ReturnsFalse() {
        _definition.Constraints.Add(new AttributeConstraint("LengthIs", 20));

        var result = _attribute.IsValid;

        result.Should().BeFalse();
    }

    [Fact]
    public void IsValid_WithInvalidArgument_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("LengthIs", "wrong"));

        var action = () => _attribute.IsValid.Should().BeFalse();

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void IsValid_WithInvalidNumberOfArguments_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("LengthIs"));

        var action = () => _attribute.IsValid;

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void IsValid_WithInvalidConstraint_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("Invalid", 20));

        var action = () => _attribute.IsValid;

        action.Should().Throw<ArgumentException>();
    }
}
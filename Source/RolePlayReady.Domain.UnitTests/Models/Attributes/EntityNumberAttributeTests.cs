namespace RolePlayReady.Models.Attributes;

public class EntityNumberAttributeTests {
    private readonly AttributeDefinition<int> _definition;
    private readonly EntityNumberAttribute<int> _attribute;

    public EntityNumberAttributeTests() {
        _definition = new AttributeDefinition<int> {
            Name = "TestName",
            Description = "TestDescription",
        };

        _attribute = new EntityNumberAttribute<int> {
            Attribute = _definition,
            Value = 42
        };
    }

    [Fact]
    public void Constructor_InitializesProperties() {
        _attribute.Attribute.Should().Be(_definition);
        _attribute.Value.Should().Be(42);
        _attribute.IsValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_WithValidConstraint_ReturnsTrue() {
        _definition.Constraints.Add(new AttributeConstraint("EqualTo", 42));

        var result = _attribute.IsValid;

        result.Should().BeTrue();
    }

    [Fact]
    public void IsValid_FailedConstraint_ReturnsFalse() {
        _definition.Constraints.Add(new AttributeConstraint("EqualTo", 20));

        var result = _attribute.IsValid;

        result.Should().BeFalse();
    }

    [Fact]
    public void IsValid_WithInvalidArgument_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("EqualTo", "wrong"));

        var action = () => _attribute.IsValid.Should().BeFalse();

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void IsValid_WithInvalidNumberOfArguments_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("EqualTo"));

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
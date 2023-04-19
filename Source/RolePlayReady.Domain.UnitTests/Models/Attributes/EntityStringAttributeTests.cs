using NSubstitute.Core;

namespace RolePlayReady.Models.Attributes;

public class EntityStringAttributeTests {
    private readonly AttributeDefinition _definition;
    private readonly EntityStringAttribute _attribute;

    public EntityStringAttributeTests() {
        _definition = new AttributeDefinition {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(string)
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
        _attribute.Validate().IsSuccessful.Should().BeTrue();
    }

    [Theory]
    [InlineData("LengthIs", 9)]
    [InlineData("MinimumLengthIs", 2)]
    [InlineData("MaximumLengthIs", 20)]
    public void IsValid_WithValidConstraint_ReturnsTrue(string validator, int argument) {
        _definition.Constraints.Add(new AttributeConstraint(validator, argument));

        _attribute.Validate().IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void IsValid_FailedConstraint_ReturnsFalse() {
        _definition.Constraints.Add(new AttributeConstraint("LengthIs", 20));

        _attribute.Validate().IsSuccessful.Should().BeFalse();
    }

    [Fact]
    public void IsValid_WithInvalidArgument_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("LengthIs", "wrong"));

        var action = () => _attribute.Validate().IsSuccessful.Should().BeFalse();

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void IsValid_WithInvalidNumberOfArguments_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("LengthIs"));

        var action = () => _attribute.Validate().IsSuccessful;

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void IsValid_WithInvalidConstraint_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("Invalid", 20));

        var action = () => _attribute.Validate().IsSuccessful;

        action.Should().Throw<ArgumentException>();
    }
}
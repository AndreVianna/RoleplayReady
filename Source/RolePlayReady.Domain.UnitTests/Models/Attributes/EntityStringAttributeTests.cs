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
        _attribute.Validate().IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData("LengthIs", 9)]
    [InlineData("MinimumLengthIs", 2)]
    [InlineData("MaximumLengthIs", 20)]
    public void Validate_WithValidConstraint_ReturnsTrue(string validator, int argument) {
        _definition.Constraints.Add(new AttributeConstraint(validator, argument));

        _attribute.Validate().IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Validate_FailedConstraint_ReturnsFalse() {
        _definition.Constraints.Add(new AttributeConstraint("LengthIs", 20));

        _attribute.Validate().IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Validate_WithInvalidArgument_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("LengthIs", "wrong"));

        var action = _attribute.Validate;

        action.Should().Throw<ArgumentException>().WithMessage("Invalid type of arguments[0] of 'LengthIs'. Expected: Integer. Found: String. (Parameter 'arguments[0]')");
    }

    [Fact]
    public void Validate_WithInvalidNumberOfArguments_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("LengthIs"));

        var action = _attribute.Validate;

        action.Should().Throw<ArgumentException>().WithMessage("Invalid number of arguments for 'LengthIs'. Missing argument 0. (Parameter 'arguments')");
    }

    [Fact]
    public void Validate_WithInvalidConstraint_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("Invalid", 20));

        var action = _attribute.Validate;

        action.Should().Throw<InvalidOperationException>().WithMessage("Unsupported validator 'Invalid'.");
    }
}
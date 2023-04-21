namespace RolePlayReady.Models.Attributes;

public class EntityListAttributeTests {
    private readonly AttributeDefinition _definition;
    private readonly EntityListAttribute<string> _attribute;

    public EntityListAttributeTests() {
        _definition = new AttributeDefinition {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(List<string>),
        };

        _attribute = new EntityListAttribute<string> {
            Attribute = _definition,
            Value = new List<string> { "TestValue" }
        };
    }

    [Fact]
    public void Constructor_InitializesProperties() {
        _attribute.Attribute.Should().Be(_definition);
        _attribute.Value.Should().BeEquivalentTo("TestValue");
        _attribute.Validate().IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData("CountIs", 1)]
    [InlineData("MinimumCountIs", 1)]
    [InlineData("MaximumCountIs", 10)]
    public void Validate_WithValidConstraint_ReturnsTrue(string validator, int argument) {
        _definition.Constraints.Add(new AttributeConstraint(validator, argument));

        _attribute.Validate().IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Validate_FailedConstraint_ReturnsFalse() {
        _definition.Constraints.Add(new AttributeConstraint("CountIs", 20));

        _attribute.Validate().IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Validate_WithInvalidArgument_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("CountIs", "wrong"));

        var action = _attribute.Validate;

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Validate_WithInvalidNumberOfArguments_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("CountIs"));

        var action = _attribute.Validate;

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Validate_WithInvalidConstraint_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("Invalid", 20));

        var action = _attribute.Validate;

        action.Should().Throw<InvalidOperationException>();
    }
}
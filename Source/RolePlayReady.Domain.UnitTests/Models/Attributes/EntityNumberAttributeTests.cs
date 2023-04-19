namespace RolePlayReady.Models.Attributes;

public class EntityNumberAttributeTests {
    private readonly AttributeDefinition _definition;
    private readonly EntityNumberAttribute<int> _attribute;

    public EntityNumberAttributeTests() {
        _definition = new AttributeDefinition {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(int),
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
        _attribute.Validate().IsSuccessful.Should().BeTrue();
    }

    [Theory]
    [InlineData("IsEqualTo", 42)]
    [InlineData("MinimumIs", 2)]
    [InlineData("MaximumIs", 99)]
    [InlineData("IsLessThan", 99)]
    [InlineData("IsGreaterThan", 2)]
    public void IsValid_WithValidConstraint_ReturnsTrue(string validator, int argument) {
        _definition.Constraints.Add(new AttributeConstraint(validator, argument));

        _attribute.Validate().IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void IsValid_WithInvalidArgument_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("EqualTo", "wrong"));

        var action = () => _attribute.Validate().IsSuccessful.Should().BeFalse();

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void IsValid_WithInvalidNumberOfArguments_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("EqualTo"));

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
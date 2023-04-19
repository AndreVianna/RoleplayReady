namespace RolePlayReady.Models.Attributes;

public class EntityListAttributeTests {
    private readonly AttributeDefinition<List<string>> _definition;
    private readonly EntityListAttribute<string> _attribute;

    public EntityListAttributeTests() {
        _definition = new AttributeDefinition<List<string>> {
            Name = "TestName",
            Description = "TestDescription",
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
        _attribute.IsValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_WithValidConstraint_ReturnsTrue() {
        _definition.Constraints.Add(new AttributeConstraint("CountIs", 1));

        var result = _attribute.IsValid;

        result.Should().BeTrue();
    }

    [Fact]
    public void IsValid_FailedConstraint_ReturnsFalse() {
        _definition.Constraints.Add(new AttributeConstraint("CountIs", 20));

        var result = _attribute.IsValid;

        result.Should().BeFalse();
    }

    [Fact]
    public void IsValid_WithInvalidArgument_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("CuntIs", "wrong"));

        var action = () => _attribute.IsValid.Should().BeFalse();

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void IsValid_WithInvalidNumberOfArguments_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("CountIs"));

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
namespace RolePlayReady.Models.Attributes;

public class EntityDictionaryAttributeTests {
    private readonly AttributeDefinition<Dictionary<string, int>> _definition;
    private readonly EntityDictionaryAttribute<string, int> _attribute;

    public EntityDictionaryAttributeTests() {
        _definition = new AttributeDefinition<Dictionary<string, int>> {
            Name = "TestName",
            Description = "TestDescription",
        };

        _attribute = new EntityDictionaryAttribute<string, int> {
            Attribute = _definition,
            Value = new Dictionary<string, int> { ["TestValue"] = 42 }
        };
    }

    [Fact]
    public void Constructor_InitializesProperties() {
        _attribute.Attribute.Should().Be(_definition);
        _attribute.Value.Should().BeEquivalentTo(new Dictionary<string, int> { ["TestValue"] = 42 });
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
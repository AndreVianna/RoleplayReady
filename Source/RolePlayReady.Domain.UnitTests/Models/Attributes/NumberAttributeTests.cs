namespace RolePlayReady.Models.Attributes;

public class NumberAttributeTests {
    private readonly AttributeDefinition _definition;
    private readonly NumberAttribute<int> _attribute;

    public NumberAttributeTests() {
        _definition = new() {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(int),
        };

        _attribute = new() {
            Definition = _definition,
            Value = 42
        };
    }

    [Fact]
    public void Constructor_InitializesProperties() {
        _attribute.Definition.Should().Be(_definition);
        _attribute.Value.Should().Be(42);
        _attribute.Validate().IsSuccess.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Validate_WithValidConstraint_ReturnsTrue(string validator, object[] arguments, bool expectedResult) {
        _definition.Constraints.Add(new AttributeConstraint(validator, arguments));

        _attribute.Validate().IsSuccess.Should().Be(expectedResult);
    }

    private class TestData : TheoryData<string, object[], bool> {
        public TestData() {
            Add("MaximumIs", new object[] { 99 }, true);
            Add("MaximumIs", new object[] { 2 }, false);
            Add("MinimumIs", new object[] { 2 }, true);
            Add("MinimumIs", new object[] { 99 }, false);
            Add("IsLessThan", new object[] { 99 }, true);
            Add("IsLessThan", new object[] { 2 }, false);
            Add("IsGreaterThan", new object[] { 2 }, true);
            Add("IsGreaterThan", new object[] { 99 }, false);
            Add("IsEqualTo", new object[] { 42 }, true);
            Add("IsEqualTo", new object[] { 13 }, false);
        }
    }

    [Fact]
    public void Validate_WithInvalidArgument_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("IsEqualTo", "wrong"));

        var action = _attribute.Validate;

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Validate_WithInvalidNumberOfArguments_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("IsEqualTo"));

        var action = _attribute.Validate;

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Validate_WithInvalidConstraint_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("Invalid"));

        var action = _attribute.Validate;

        action.Should().Throw<InvalidOperationException>();
    }
}
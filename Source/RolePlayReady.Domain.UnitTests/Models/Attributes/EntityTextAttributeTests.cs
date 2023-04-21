using System.Validators.Collection;
using System.Validators.Number;
using System.Validators.Text;

namespace RolePlayReady.Models.Attributes;

public class EntityTextAttributeTests {
    private readonly AttributeDefinition _definition;
    private readonly EntityTextAttribute _attribute;

    public EntityTextAttributeTests() {
        _definition = new() {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(string)
        };

        _attribute = new() {
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
    [ClassData(typeof(TestData))]
    public void Validate_WithValidConstraint_ReturnsTrue(string validator, object[] arguments, bool expectedResult) {
        _definition.Constraints.Add(new AttributeConstraint(validator, arguments));

        _attribute.Validate().IsSuccess.Should().Be(expectedResult);
    }

    private class TestData : TheoryData<string, object[], bool> {
        public TestData() {
            Add("MaximumLengthIs", new object[] { 20 }, true);
            Add("MaximumLengthIs", new object[] { 2 }, false);
            Add("MinimumLengthIs", new object[] { 2 }, true);
            Add("MinimumLengthIs", new object[] { 20 }, false);
            Add("LengthIs", new object[] { 9 }, true);
            Add("LengthIs", new object[] { 20 }, false);
            Add("IsOneOf", new object[] { "One", "TestValue", "Three" }, true);
            Add("IsOneOf", new object[] { "One", "Two", "Three" }, false);
        }
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
        _definition.Constraints.Add(new AttributeConstraint("Invalid"));

        var action = _attribute.Validate;

        action.Should().Throw<InvalidOperationException>().WithMessage("Unsupported validator 'Invalid'.");
    }
}
namespace RolePlayReady.Models.Attributes;

public class ListAttributeTests {
    private readonly AttributeDefinition _definition;
    private readonly ListAttribute<string> _attribute;

    public ListAttributeTests() {
        _definition = new() {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(List<string>),
        };

        _attribute = new() {
            Definition = _definition,
            Value = ["TestValue1", "TestValue2", "TestValue3"]
        };
    }

    [Fact]
    public void Constructor_InitializesProperties() {
        _attribute.Definition.Should().Be(_definition);
        _attribute.Value.Should().BeEquivalentTo("TestValue1", "TestValue2", "TestValue3");
        _attribute.Validate().IsSuccess.Should().BeTrue();
    }

    private class TestData : TheoryData<string, object[], bool> {
        public TestData() {
            Add("Has", [3], true);
            Add("Has", [13], false);
            Add("HasAtLeast", [1], true);
            Add("HasAtLeast", [99], false);
            Add("HasAtMost", [99], true);
            Add("HasAtMost", [1], false);
            Add("Contains", ["TestValue2"], true);
            Add("Contains", ["TestValue13"], false);
        }
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Validate_WithValidConstraint_ReturnsTrue(string validator, object[] arguments, bool expectedResult) {
        _definition.Constraints.Add(new AttributeConstraint(validator, arguments));

        _attribute.Validate().IsSuccess.Should().Be(expectedResult);
    }

    [Fact]
    public void Validate_WithInvalidArgument_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("Has", "wrong"));

        _attribute.Invoking(x => x.Validate()).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Validate_WithInvalidNumberOfArguments_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("Has"));

        _attribute.Invoking(x => x.Validate()).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Validate_WithInvalidConstraint_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("Invalid"));

        _attribute.Invoking(x => x.Validate()).Should().Throw<InvalidOperationException>();
    }
}
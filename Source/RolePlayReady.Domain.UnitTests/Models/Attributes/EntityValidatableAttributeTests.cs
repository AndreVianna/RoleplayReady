using static System.Results.ValidationResult;

namespace RolePlayReady.Models.Attributes;

public class EntityValidatableAttributeTests {
    private readonly AttributeDefinition _definition;
    private readonly EntityValidatableAttribute<TestObject> _attribute;
    private readonly TestObject _testObject = new TestObject("Hello");

    private record TestObject(string Name) : IValidatable {
        public ValidationResult Validate() => Success;
    }

    public EntityValidatableAttributeTests() {
        _definition = new AttributeDefinition {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(TestObject)
        };

        _attribute = new EntityValidatableAttribute<TestObject> {
            Attribute = _definition,
            Value = new TestObject("Hello")
        };
    }

    [Fact]
    public void Constructor_InitializesProperties() {
        _attribute.Attribute.Should().Be(_definition);
        _attribute.Value.Should().Be(_testObject);
        _attribute.Validate().IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithInvalidConstraint_ThrowsArgumentException() {
        _definition.Constraints.Add(new AttributeConstraint("Invalid", 20));

        var action = _attribute.Validate;

        action.Should().Throw<InvalidOperationException>();
    }
}
using static System.Results.ValidationResult;

namespace RolePlayReady.Models.Attributes;

public class EntityValidatableAttributeTests {
    private readonly AttributeDefinition _definition;
    private readonly EntityValidatableAttribute<TestObject> _attribute;
    private readonly TestObject _testObject = new("Hello");

    private record TestObject(string Name) : IValidatable {
        public ValidationResult Validate() => Success;
    }

    public EntityValidatableAttributeTests() {
        _definition = new() {
            Name = "TestName",
            Description = "TestDescription",
            DataType = typeof(TestObject)
        };

        _attribute = new() {
            Attribute = _definition,
            Value = new("Hello")
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
        _definition.Constraints.Add(new AttributeConstraint("Invalid"));

        var action = _attribute.Validate;

        action.Should().Throw<InvalidOperationException>();
    }
}
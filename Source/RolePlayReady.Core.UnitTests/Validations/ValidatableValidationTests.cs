namespace System.Validations;

public class ValidatableValidationTests {
    public record ChildObject : IValidatable {
        public required string Name { get; init; }
        public ValidationResult Validate() {
            var result = ValidationResult.AsSuccess();
            result += Name.IsNotNull()
                .And.LengthIs(5).Result;
            return result;
        }
    }

    public record TestObject : IValidatable {
        public required ChildObject Child { get; init; }
        public ValidationResult Validate() {
            var result = ValidationResult.AsSuccess();
            result += Child.IsNotNull()
                .And.IsValid().Result;
            return result;
        }
    }

    private class TestData : TheoryData<TestObject, bool, int> {
        public TestData() {
            Add(new() { Child = new() { Name = "Mario" } }, true, 0);
            Add(new() { Child = new() { Name = default! } }, false, 1);
            Add(new() { Child = default! }, false, 1);
        }
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Validate_Validates(TestObject subject, bool isSuccess, int errorCount) {
        // Act
        var result = subject.Validate();

        // Assert
        result.IsSuccess.Should().Be(isSuccess);
        result.Errors.Should().HaveCount(errorCount);
    }
}
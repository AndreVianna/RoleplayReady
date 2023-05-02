namespace System.Validations;

public class TypeValidationTests {
    public record TestObject : IValidatable {
        public Type? Type { get; init; }
        public ValidationResult Validate() {
            var result = ValidationResult.Success;
            result += Type.IsNotNull()
                .And.IsEqualTo<string>().Result;
            return result;
        }
    }

    private class TestData : TheoryData<TestObject, bool, int> {
        public TestData() {
            Add(new() { Type = typeof(string) }, true, 0);
            Add(new() { Type = null }, false, 1);
            Add(new() { Type = typeof(int) }, false, 1);
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
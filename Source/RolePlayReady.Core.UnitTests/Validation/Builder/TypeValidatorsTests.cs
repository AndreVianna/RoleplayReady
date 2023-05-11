namespace System.Validation.Builder;

public class TypeValidatorsTests {
    public record TestObject : IValidatable {
        public Type? Type { get; init; }

        public ValidationResult ValidateSelf(bool negate = false) {
            var result = ValidationResult.Success();
            result += Type.Is()
                .And().IsEqualTo<string>().Result;
            return result;
        }
    }

    private class TestData : TheoryData<TestObject, int> {
        public TestData() {
            Add(new() { Type = typeof(string) }, 0);
            Add(new() { Type = null }, 1);
            Add(new() { Type = typeof(int) }, 1);
        }
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Validate_Validates(TestObject subject, int errorCount) {
        // Act
        var result = subject.ValidateSelf();

        // Assert
        result.Errors.Should().HaveCount(errorCount);
    }
}
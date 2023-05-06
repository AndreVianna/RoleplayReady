namespace System.Validation.Builder;

public class IntegerValidatorsTests {
    public record TestObject : IValidatable {
        public int Number { get; init; }
        public int? Nullable { get; init; }
        public int? Required { get; init; }

        public ValidationResult ValidateSelf(bool negate = false) {
            var result = ValidationResult.Success();
            result += Number.IsRequired().And().IsGreaterThan(10).And().IsLessThan(20).And().IsEqualTo(15).Result;
            result += Nullable.IsOptional().And().IsGreaterThan(10).And().IsLessThan(20).And().IsEqualTo(15).Result;
            result += Required.IsRequired().And().MinimumIs(10).And().MaximumIs(20).Result;
            return result;
        }
    }

    private class TestData : TheoryData<TestObject, int> {
        public TestData() {
            Add(new() { Number = 15, Nullable = 15, Required = 15, }, 0);
            Add(new() { Number = 0, Nullable = null, Required = null, }, 3);
            Add(new() { Number = 5, Nullable = 5, Required = 5, }, 5);
            Add(new() { Number = 25, Nullable = 25, Required = 25, }, 5);
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
namespace System.Validations;

public class ComparableValidationTests {
    public record TestObject : IValidatable {
        public int Number { get; init; }
        public ICollection<ValidationError> Validate() {
            var result = ValidationResult.Success();
            result += Number.Value().IsEqualTo(15).Errors;
            return result.Errors;
        }
    }

    private class TestData : TheoryData<TestObject, bool, int> {
        public TestData() {
            Add(new() { Number = 15 }, true, 0);
            Add(new() { Number = 99 }, false, 1);
        }
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Validate_Validates(TestObject subject, bool isSuccess, int errorCount) {
        // Act
        var result = (ValidationResult)subject.Validate();

        // Assert
        result.IsSuccess.Should().Be(isSuccess);
        result.Errors.Should().HaveCount(errorCount);
    }
}
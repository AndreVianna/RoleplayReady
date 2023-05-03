namespace System.Validations;

public class CollectionValidationTests {
    public record TestObject : IValidatable {
        public required ICollection<int> Numbers { get; init; } = Array.Empty<int>();
        public required ICollection<(string Name, int Age)> Names { get; init; } = Array.Empty<(string Name, int Age)>();
        public ValidationResult Validate() {
            var result = ValidationResult.Success();
            result += Numbers.List()
                .IsNotEmpty()
                .And.MinimumCountIs(2)
                .And.MaximumCountIs(4)
                .And.CountIs(3)
                .And.Contains(5)
                .And.NotContains(13)
                .And.ForEach(item => item.Value().IsGreaterThan(0)).Result;
            result += Names.ForEach(value => value.Name.IsNotNull()).Result;
            return result;
        }
    }

    private class TestData : TheoryData<TestObject, bool, int> {
        public TestData() {
            Add(new() { Numbers = new[] { 1, 3, 5 }, Names = new [] { ("Name", 30) } }, true, 0);
            Add(new() { Numbers = Array.Empty<int>(), Names = new[] { ("Name", 30), default! } }, false, 5);
            Add(new() { Numbers = new[] { 0, 5, 10, 13, 20 }, Names = new[] { ("Name", 30) } }, false, 4);
            Add(new() { Numbers = null!, Names = null!}, false, 2);
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
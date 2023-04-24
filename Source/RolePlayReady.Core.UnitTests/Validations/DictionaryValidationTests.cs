namespace System.Validations;

public class DictionaryValidationTests {
    public record TestObject : IValidatable {
        public required IDictionary<string, int> Numbers { get; init; } = new Dictionary<string, int>();
        public required IDictionary<string, string> Names { get; init; } = new Dictionary<string, string>();
        public ValidationResult Validate() {
            var result = new ValidationResult();
            result += Numbers.Map()
                .IsNotEmpty()
                .And.MinimumCountIs(2)
                .And.MaximumCountIs(4)
                .And.CountIs(3)
                .And.ContainsKey("Five")
                .And.NotContainsKey("Nine")
                .And.ForEach(item => item.Value().IsGreaterThan(0)).Result;
            result += Names.ForEach(value => value.IsNotNull()).Result;
            return result;
        }
    }

    private class TestData : TheoryData<TestObject, bool, int> {
        public TestData() {
            Add(new() { Numbers = new Dictionary<string, int> { ["One"] = 1, ["Three"] = 3, ["Five"] = 5 }, Names = new Dictionary<string, string> { ["Some"] = "Name" } }, true, 0);
            Add(new() { Numbers = new Dictionary<string, int>(), Names = new Dictionary<string, string> { ["Name"] = default! } }, false, 5);
            Add(new() { Numbers = new Dictionary<string, int> { ["One"] = 1, ["Two"] = default!, ["Three"] = 3, ["Four"] = 4, ["Nine"] = 9 }, Names = new Dictionary<string, string> { ["Some"] = "Name" } }, false, 5);
            Add(new() { Numbers = null!, Names = null! }, false, 2);
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